using System;

[Serializable]
public class AchievementDTO
{
    // ��Ÿ ������ (AchievementSO ���)
    public string ID;
    public string Name;
    public string Description;
    public EAchievementCondition Condition;
    public int GoalValue;
    public ECurrencyType RewardCurrencyType;
    public int RewardAmount;

    // ���� ������
    public int CurrentValue;
    public bool RewardClaimed;

    public AchievementDTO() { }

    public AchievementDTO(Achievement achievement)
    {
        ID = achievement.ID;
        Name = achievement.Name;
        Description = achievement.Description;
        Condition = achievement.Condition;
        GoalValue = achievement.GoalValue;
        RewardCurrencyType = achievement.RewardCurrencyType;
        RewardAmount = achievement.RewardAmount;

        CurrentValue = achievement.CurrentValue;
        RewardClaimed = achievement.RewardClaimed;
    }

    public bool CanClaimReward()
    {
        return RewardClaimed == false && CurrentValue >= GoalValue;
    }
}
