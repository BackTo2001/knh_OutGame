using System;
using System.Collections.Generic;

public class Attendance
{
    public readonly string Id;

    public readonly DateTime StartDate;                                   // 이 출석이 언제부터 시작할 것인가?
    public int DayCount { get; private set; }                    // 출석일
    public DateTime LastAttendanceDate { get; private set; }     // 마지막 출석일

    private List<AttendanceReward> _rewards;

    public Attendance(string id, DateTime startDate, DateTime lastAttendanceDate, int dayCount)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new Exception("출석 ID가 null이거나 비어 있습니다.");
        }

        if (startDate == new DateTime())
        {
            throw new Exception("출석 시작일 startDate가 지정되지 않았습니다.");
        }

        if (dayCount <= 0)
        {
            throw new Exception("출석일은 0보다 작을 수 없습니다.");
        }

        if (lastAttendanceDate == new DateTime())
        {
            throw new Exception("마지막 출석일 lastAttendanceDate가 null입니다.");
        }

        if (lastAttendanceDate < startDate)
        {
            throw new Exception("마지막 출석일 lastAttendanceDate는 시작일 startDate보다 이전일 수 없습니다.");
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
            throw new Exception("출석 체크 날짜 date가 지정되지 않았습니다.");
        }


        // ToDo : year과 month도 비교해야 함 => Day를 Date로 변경 
        if (LastAttendanceDate.Date < date.Date)
        {
            // date.Day는 일(day)만 정수로 반환 : 11
            // date.Date는 연/월/일 : 2025-08-11 00:00:00 형태로 반환
            DayCount += 1;
            LastAttendanceDate = date;
        }
    }

    public void AddReward(AttendanceReward reward)
    {
        if (reward == null)
        {
            throw new Exception("출석 보상은 null일 수 없습니다.");
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
            throw new Exception("출석 인덱스가 올바르지 않습니다.");
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
