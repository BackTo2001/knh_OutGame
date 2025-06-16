using System;
using System.Collections.Generic;

public class Attendance
{
    public readonly string Id;

    public readonly DateTime StartDate;                                   // �� �⼮�� �������� ������ ���ΰ�?
    public int DayCount { get; private set; }                    // �⼮��
    public DateTime LastAttendanceDate { get; private set; }     // ������ �⼮��

    private List<AttendanceReward> _rewards;

    public Attendance(string id, DateTime startDate, DateTime lastAttendanceDate, int dayCount)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new Exception("�⼮ ID�� null�̰ų� ��� �ֽ��ϴ�.");
        }

        if (startDate == new DateTime())
        {
            throw new Exception("�⼮ ������ startDate�� �������� �ʾҽ��ϴ�.");
        }

        if (dayCount <= 0)
        {
            throw new Exception("�⼮���� 0���� ���� �� �����ϴ�.");
        }

        if (lastAttendanceDate == new DateTime())
        {
            throw new Exception("������ �⼮�� lastAttendanceDate�� null�Դϴ�.");
        }

        if (lastAttendanceDate < startDate)
        {
            throw new Exception("������ �⼮�� lastAttendanceDate�� ������ startDate���� ������ �� �����ϴ�.");
        }

        Id = id;
        StartDate = startDate;
        DayCount = dayCount;
        LastAttendanceDate = lastAttendanceDate;

        _rewards = new List<AttendanceReward>();
    }

    public void Check(DateTime date)
    {
        if (date == new DateTime())
        {
            throw new Exception("�⼮ üũ ��¥ date�� �������� �ʾҽ��ϴ�.");
        }


        // ToDo : year�� month�� ���ؾ� �� => Day�� Date�� ���� 
        if (LastAttendanceDate.Date < date.Date)
        {
            // date.Day�� ��(day)�� ������ ��ȯ : 11
            // date.Date�� ��/��/�� : 2025-08-11 00:00:00 ���·� ��ȯ
            DayCount += 1;
            LastAttendanceDate = date;
        }
    }

    public void AddReward(AttendanceReward reward)
    {
        if (reward == null)
        {
            throw new Exception("�⼮ ������ null�� �� �����ϴ�.");
        }

        _rewards.Add(reward);
    }

    public void GetReward(Index index)
    {

    }

    public bool TryClaim(int day)
    {
        if (day < 1 || _rewards.Count < day)
        {
            throw new Exception("�⼮ �ε����� �ùٸ��� �ʽ��ϴ�.");
        }

        if (day > DayCount)
        {
            return false;
        }
        return _rewards[day - 1].TryClaim();
    }

    public AttendanceDTO ToDTO()
    {
        return new AttendanceDTO(Id, StartDate, DayCount, LastAttendanceDate, _rewards);
    }
}
