using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;

    [SerializeField]
    private List<AchievementSO> _metaDatas;

    private List<Achievement> _achievements;

    public List<AchievementDTO> Achievements() => _achievements.ConvertAll(a => new AchievementDTO(a));

    public event Action OnDataChanged;
    public event Action<AchievementDTO> OnAchievementUpdated;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Init();
    }

    private void Init()
    {
        // 초기화
        _achievements = new List<Achievement>();


        foreach (var metaData in _metaDatas)
        {
            Achievement duplicatedAchievement = FindByID(metaData.ID);
            if (duplicatedAchievement != null)
            {
                throw new Exception($"업적 ID({metaData.ID})가 중복됩니다.");
            }
            Achievement achievement = new Achievement(metaData);
            _achievements.Add(achievement);
        }

    }

    private Achievement FindByID(string id)
    {
        return _achievements.Find(a => a.ID == id);
    }

    public void Increase(EAchievementCondition condition, int value)
    {
        foreach (var achievement in _achievements)
        {
            if (achievement.Condition == condition)
            {
                bool prevCanClaimReward = achievement.CanClaimReward();
                achievement.Increase(value);
                bool canClaimReward = achievement.CanClaimReward();

                if (prevCanClaimReward == false && canClaimReward == true)
                {
                    // 보상 획득 가능 상태로 변경되었을 때
                    Debug.Log($"Achievement '{achievement.Name}' can now claim reward.");
                    // DTO로 변환하여 이벤트 발생
                    OnAchievementUpdated?.Invoke(new AchievementDTO(achievement));
                }
            }
        }

        // 데이터 변경 이벤트 발생
        OnDataChanged?.Invoke();
    }

    public bool TryClaimReward(AchievementDTO achievementDTO)
    {
        Achievement achievement = FindByID(achievementDTO.ID);

        if (achievement == null)
        {
            return false;
        }

        if (achievement.TryClaimReward())
        {
            CurrencyManager.Instance.Add(achievement.RewardCurrencyType, achievement.RewardAmount);

            OnDataChanged?.Invoke();

            return true;
        }

        return false;
    }
}
