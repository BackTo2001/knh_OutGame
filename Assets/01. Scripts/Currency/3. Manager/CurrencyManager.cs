using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ��Ű��ó : ���� �� ��ü (���踶�� ö���� �ִ�.) (ex : MVC, MVVM, Clean Architecture ��)
// ������ ���� : ���踦 �����ϴ� �������� ���̴� ����

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    private Dictionary<ECurrencyType, Currency> _currencies;
    // public Dictionary<ECurrencyType, Currency> Currencies => _currencies;

    // �����ο� ��ȭ�� ���� �� ȣ��Ǵ� �׼�
    public event Action OnDataChanged;

    private CurrencyRepository _repository;

    private string _currentUserId => AccountManager.Instance?.CurrentAccount?.Email;

    // �ι�Ʈ C.��ƾ : �̸��ϴ� ����ȭ�� 90%�� �������. 
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

        // ����
        _currencies = new Dictionary<ECurrencyType, Currency>((int)ECurrencyType.Count);

        // �������丮(�����)
        _repository = new CurrencyRepository();

        List<CurrencyDTO> loadedCurrencies = _repository.Load(_currentUserId);
        if (loadedCurrencies == null)
        {
            for (int i = 0; i < (int)ECurrencyType.Count; ++i)
            {
                ECurrencyType type = (ECurrencyType)i;

                // ��� , ���̾Ƹ�� ���� 0 ������ ���� �� ��ųʸ��� ����
                Currency currency = new Currency(type, 0);
                _currencies.Add(type, currency);
            }
        }
        else
        {
            foreach (var data in loadedCurrencies)
            {
                // DTO�� ���������� ��ȯ
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
