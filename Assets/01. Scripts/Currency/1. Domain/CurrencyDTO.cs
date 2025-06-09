using UnityEngine;

// 계층 간 데이터 전송을 위해 도메인 모델 대신 사용되는 DTO(Data Transfer Object) 클래스.
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
