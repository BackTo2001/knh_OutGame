using UnityEngine;

public class AttendanceReward : MonoBehaviour
{
    public readonly ECurrencyType CurrencyType;
    public readonly int Amount;
    public bool Claimed { get; private set; }

    public AttendanceReward(ECurrencyType currencyType, int amount, bool claimed)
    {
        if (amount < 0)
        {
            throw new System.Exception("출석 보상 금액은 0보다 작을 수 없습니다.");
        }

        CurrencyType = currencyType;
        Amount = amount;
        Claimed = claimed;
    }

    public bool TryClaim()
    {
        if (Claimed == true)
        {
            return false; // 이미 수령한 보상은 다시 수령할 수 없음
        }

        Claimed = true; // 보상 수령 처리

        return true;
    }
}
