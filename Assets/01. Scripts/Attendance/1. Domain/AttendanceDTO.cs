using System;
using System.Collections.Generic;

public class AttendanceDTO
{
    public readonly string Id;
    public readonly DateTime StartDate;
    public readonly int DayCount;
    public readonly DateTime LastAttendanceDate;
    public readonly List<AttendanceRewardDTO> Rewards;

    public AttendanceDTO(string id, DateTime startDate, int dayCount, DateTime lastAttendanceDate, List<AttendanceReward> rewards)
    {
        Id = id;
        StartDate = startDate;
        DayCount = dayCount;
        LastAttendanceDate = lastAttendanceDate;
        Rewards = new List<AttendanceRewardDTO>(rewards.Count);

        for (int i = 0; i < rewards.Count; i++)
        {
            bool canClaim = !rewards[i].Claimed && rewards[i];
            Rewards.Add(new AttendanceRewardDTO(rewards[i].CurrencyType, rewards[i].Amount, rewards[i].Claimed, canClaim));
        }
    }
}