using System.Collections.Generic;
using UnityEngine;

public class UI_Attendance : MonoBehaviour
{
    public List<UI_AttendanceRewardSlot> Slots;

    private void Start()
    {
        Refresh();

        AttendanceManager.Instance.OnDataChanged += Refresh;
    }

    private void Refresh()
    {
        AttendanceDTO attendance = AttendanceManager.Instance.GetAttendance("3day");

        int index = 0;
        foreach (var slot in Slots)
        {
            slot.Refresh(attendance.Id, index, attendance.Rewards[0]);
            index++;
        }
    }


}
