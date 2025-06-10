using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;


    [SerializeField]
    private List<AchievementSO> _metaDatas;

    private AchievementRepository _repository;

    private List<Achievement> _achievements;
    public List<AchievementDTO> Achievements => _achievements.ConvertAll((a) => new AchievementDTO(a));

    public event Action OnDataChanged;
    public event Action<AchievementDTO> OnNewAchievementRewarded;


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
        // �ʱ�ȭ
        _repository = new AchievementRepository();

        _achievements = new List<Achievement>();

        List<AchievementSaveData> saveDatas = _repository.Load();
        // ������ �ʱ�ȭ
        foreach (var metaData in _metaDatas)
        {
            // �ߺ� �˻�
            Achievement duplicatedAchievement = FindByID(metaData.ID);
            if (duplicatedAchievement != null)
            {
                throw new Exception($"���� ID({metaData.ID})�� �ߺ��˴ϴ�.");
            }

            // ������ ����
            AchievementSaveData saveData = saveDatas?.Find(a => a.ID == metaData.ID) ?? new AchievementSaveData();
            Achievement achievement = new Achievement(metaData, saveData);
            _achievements.Add(achievement);
        }
    }

    private Achievement FindByID(string id)
    {
        return _achievements.Find((a) => a.ID == id);
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
                    // �̶��� �ٷ� ���ο� ������ ������ �����Ҷ� 
                    // UI_AchievementNotification.Instance.Show(achievement);
                    OnNewAchievementRewarded?.Invoke(new AchievementDTO(achievement));
                }
            }
        }

        _repository.Save(Achievements);

        OnDataChanged?.Invoke();
    }

    public bool TryClaimReward(AchievementDTO achievementDto)
    {
        Achievement achievement = FindByID(achievementDto.ID);
        if (achievement == null)
        {
            return false;
        }

        if (achievement.TryClaimReward())
        {
            CurrencyManager.Instance.Add(achievement.RewardCurrencyType, achievement.RewardAmount);

            _repository.Save(Achievements);

            OnDataChanged?.Invoke();

            return true;
        }

        return false;
    }
}