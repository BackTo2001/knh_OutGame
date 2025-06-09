using System;
using System.Collections.Generic;
using UnityEngine;

// 아키텍처 : 설계 그 자체 (설계마다 철학이 있다.) (ex : MVC, MVVM, Clean Architecture 등)
// 디자인 패턴 : 설계를 구현하는 과정에서 쓰이는 패턴

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    private Dictionary<ECurrencyType, Currency> _currencies;
    // public Dictionary<ECurrencyType, Currency> Currencies => _currencies;

    public event Action OnDataChanged;

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
        _currencies = new Dictionary<ECurrencyType, Currency>();

        for (int i = 0; i < (int)ECurrencyType.Count; i++)
        {
            ECurrencyType type = (ECurrencyType)i;

            // 골드 , 다이아몬드 등을 0 값으로 생성 후 딕셔너리에 삽입
            Currency currency = new Currency(type, 0);
            _currencies.Add(type, currency);
        }
    }

    public CurrencyDTO Get(ECurrencyType type)
    {
        return new CurrencyDTO(_currencies[type]);
    }

    public void Add(ECurrencyType type, int value)
    {
        _currencies[type].Add(value);

        OnDataChanged?.Invoke();
    }

    public void Subtract(ECurrencyType type, int value)
    {
        _currencies[type].Subtract(value);

        OnDataChanged?.Invoke();
    }


}
