using UnityEngine;

// ���� �� ������ ������ ���� ������ �� ��� ���Ǵ� DTO(Data Transfer Object) Ŭ����.
public class CurrencyDTO : MonoBehaviour
{
    public readonly ECurrencyType Type;
    public readonly int Value;

    public CurrencyDTO(Currency currency)
    {
        Type = currency.Type;
        Value = currency.Value;
    }
}
