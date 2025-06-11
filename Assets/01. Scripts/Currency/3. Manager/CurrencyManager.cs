using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 아키텍처 : 설계 그 자체 (설계마다 철학이 있다.) (ex : MVC, MVVM, Clean Architecture 등)
// 디자인 패턴 : 설계를 구현하는 과정에서 쓰이는 패턴

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    private Dictionary<ECurrencyType, Currency> _currencies;
    // public Dictionary<ECurrencyType, Currency> Currencies => _currencies;

    // 도메인에 변화가 있을 때 호출되는 액션
    public event Action OnDataChanged;

    private CurrencyRepository _repository;

    private string _currentUserId => AccountManager.Instance?.CurrentAccount?.Email;

    // 로버트 C.마틴 : 미리하는 최적화의 90%는 쓸모없다. 
    // public event Action OnGoldDataChanged;
    // public event Action OnDiamondDataChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Init();
    }

    private void Init()
    {

        // 생성
        _currencies = new Dictionary<ECurrencyType, Currency>((int)ECurrencyType.Count);

        // 레포지토리(깃허브)
        _repository = new CurrencyRepository();

        List<CurrencyDTO> loadedCurrencies = _repository.Load(_currentUserId);
        if (loadedCurrencies == null)
        {
            for (int i = 0; i < (int)ECurrencyType.Count; ++i)
            {
                ECurrencyType type = (ECurrencyType)i;

                // 골드 , 다이아몬드 등을 0 값으로 생성 후 딕셔너리에 삽입
                Currency currency = new Currency(type, 0);
                _currencies.Add(type, currency);
            }
        }
        else
        {
            foreach (var data in loadedCurrencies)
            {
                // DTO를 도메인으로 변환
                Currency currency = new Currency(data.Type, data.Value);
                _currencies.Add(data.Type, currency);
            }
        }
    }

    private List<CurrencyDTO> ToDTOList()
    {
        return _currencies.ToList().ConvertAll(currency => new CurrencyDTO(currency.Value));
    }

    public CurrencyDTO Get(ECurrencyType type)
    {
        return new CurrencyDTO(_currencies[type]);
    }

    public void Add(ECurrencyType type, int value)
    {
        _currencies[type].Add(value);

        if (!string.IsNullOrEmpty(_currentUserId))
            _repository.Save(ToDTOList(), _currentUserId);

        AchievementManager.Instance.Increase(EAchievementCondition.GoldCollect, value);

        OnDataChanged?.Invoke();
    }

    public bool TryBuy(ECurrencyType type, int value)
    {
        if (!_currencies[type].TryBuy(value))
        {
            return false;
        }

        if (!string.IsNullOrEmpty(_currentUserId))
            _repository.Save(ToDTOList(), _currentUserId);

        OnDataChanged?.Invoke();

        return true;
    }

}
