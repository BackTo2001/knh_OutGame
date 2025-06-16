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
            throw new System.Exception("�⼮ ���� �ݾ��� 0���� ���� �� �����ϴ�.");
        }

        CurrencyType = currencyType;
        Amount = amount;
        Claimed = claimed;
    }

    public bool TryClaim()
    {
        if (Claimed == true)
        {
            return false; // �̹� ������ ������ �ٽ� ������ �� ����
        }

        Claimed = true; // ���� ���� ó��

        return true;
    }
}
