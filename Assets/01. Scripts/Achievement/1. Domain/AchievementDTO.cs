using System;

[Serializable]
public class AchievementDTO
{
    // 메타 데이터 (AchievementSO 기반)
    public string ID;
    public string Name;
    public string Description;
    public EAchievementCondition Condition;
    public int GoalValue;
    public int CurrentValue;
    public bool RewardClaimed;
    public ECurrencyType RewardCurrencyType;
    public int RewardAmount;

    public AchievementDTO(Achievement achievement)
    {
        ID = achievement.ID;
        Name = achievement.Name;
        Description = achievement.Description;
        Condition = achievement.Condition;
        GoalValue = achievement.GoalValue;
        CurrentValue = achievement.CurrentValue;
        RewardClaimed = achievement.RewardClaimed;
        RewardCurrencyType = achievement.RewardCurrencyType;
        RewardAmount = achievement.RewardAmount;
    }

    public bool CanClaimReward()
    {
        return RewardClaimed == false && CurrentValue >= GoalValue;
    }
}
