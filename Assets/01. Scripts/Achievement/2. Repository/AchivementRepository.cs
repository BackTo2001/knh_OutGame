using System.Collections.Generic;
using UnityEngine;

public class AchivementRepository
{
    private const string SAVE_KEY = nameof(AchivementRepository);

    public void Save(List<Achievement> achievements)
    {
        AchievementSaveDataList datas = new AchievementSaveDataList();
        datas.DataList = achievements.ConvertAll(achievement => new AchievementSaveData
        {
            ID = achievement.ID,
            CurrentValue = achievement.CurrentValue,
            RewardClaimed = achievement.RewardClaimed
        });

        string json = JsonUtility.ToJson(datas);
        PlayerPrefs.SetString(SAVE_KEY, json);
    }

    public List<AchievementSaveData> Load()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
        {
            return null;
        }

        string json = PlayerPrefs.GetString(SAVE_KEY);
        AchievementSaveDataList datas = JsonUtility.FromJson<AchievementSaveDataList>(json);

        return datas.DataList;
    }
}


public struct AchievementSaveData
{
    public string ID;
    public int CurrentValue;
    public bool RewardClaimed;

}

public struct AchievementSaveDataList
{
    public List<AchievementSaveData> DataList;
}