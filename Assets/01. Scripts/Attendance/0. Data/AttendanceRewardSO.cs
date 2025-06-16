using UnityEngine;

[CreateAssetMenu(fileName = "AttendanceRewardSO", menuName = "Scriptable Objects/AttendanceRewardSO")]
public class AttendanceRewardSO : ScriptableObject
{
    [SerializeField]
    private ECurrencyType _currencyType;
    public ECurrencyType CurrencyType => _currencyType;

    [SerializeField]
    private int _amount;
    public int Amount => _amount;
}
