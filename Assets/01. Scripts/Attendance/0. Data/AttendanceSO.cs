using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttendanceSO", menuName = "Scriptable Objects/AttendanceSO")]
public class AttendanceSO : ScriptableObject
{
    [SerializeField]
    private string _id;
    public string Id => _id;

    [SerializeField]
    private string _startDate;
    public DateTime StartDate => DateTime.Parse(_startDate);

    [SerializeField]
    private List<AttendanceRewardSO> _attendanceRewards;
    public List<AttendanceRewardSO> AttendanceRewards => _attendanceRewards;
}
