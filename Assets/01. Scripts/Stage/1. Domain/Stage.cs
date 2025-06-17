using System;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public int LevelNumber { get; private set; } // 현재 레벨 번호

    private StageLevel _currentLevel;
    private float _progressTime; // 레벨 진행 시간    

    public List<StageLevel> Levels { get; private set; } = new List<StageLevel>();

    public Stage(int levelNumber, float progressTime, List<StageLevelSO> levelSOList)
    {
        if (levelNumber < 0)
        {
            throw new Exception("레벨 번호는 0 이상이어야 합니다.");
        }
        if (progressTime < 0)
        {
            throw new Exception("올바르지 않은 진행 시간입니다.");
        }
        if (levelSOList == null || levelSOList.Count == 0)
        {
            throw new Exception("레벨 데이터가 비어있습니다.");
        }

        LevelNumber = levelNumber;
        _progressTime = progressTime;
    }

    private void AddLevel(StageLevel level)
    {
        if (level == null)
        {
            throw new Exception("레벨은 null일 수 없습니다.");
        }

        Levels.Add(level);
    }

    public void Progress(float dt)
    {
        _progressTime += dt;

        if (_currentLevel.TryLevelUp(_progressTime))
        {
            _progressTime = 0;

            if (_currentLevel.IsClear())
            {
                LevelNumber += 1;
                _currentLevel = Levels[LevelNumber - 1];
            }
        }
    }

}
