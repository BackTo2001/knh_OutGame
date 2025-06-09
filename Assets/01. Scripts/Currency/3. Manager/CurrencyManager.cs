using System.Collections.Generic;
using UnityEngine;

// ��Ű��ó : ���� �� ��ü (���踶�� ö���� �ִ�.) (ex : MVC, MVVM, Clean Architecture ��)
// ������ ���� : ���踦 �����ϴ� �������� ���̴� ����

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    private Dictionary<ECurrencyType, Currency> _currencies;
    public Dictionary<ECurrencyType, Currency> Currencies => _currencies;

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
        _currencies = new Dictionary<ECurrencyType, Currency>();

        for (int i = 0; i < (int)ECurrencyType.Count; i++)
        {
            ECurrencyType type = (ECurrencyType)i;

            // ��� , ���̾Ƹ�� ���� 0 ������ ���� �� ��ųʸ��� ����
            Currency currency = new Currency(type, 0);
            _currencies.Add(type, currency);
        }
    }

    public void Add(ECurrencyType type, int value)
    {
        _currencies[type].Add(value);

        Debug.Log($"{type} : {_currencies[type].Value}");
    }

    public void Subtract(ECurrencyType type, int value)
    {
        _currencies[type].Subtract(value);
    }
}
