using System;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public int LevelNumber { get; private set; } // ���� ���� ��ȣ

    private StageLevel _currentLevel;
    private float _progressTime; // ���� ���� �ð�    

    public List<StageLevel> Levels { get; private set; } = new List<StageLevel>();

    public Stage(int levelNumber, float progressTime, List<StageLevelSO> levelSOList)
    {
        if (levelNumber < 0)
        {
            throw new Exception("���� ��ȣ�� 0 �̻��̾�� �մϴ�.");
        }
        if (progressTime < 0)
        {
            throw new Exception("�ùٸ��� ���� ���� �ð��Դϴ�.");
        }
        if (levelSOList == null || levelSOList.Count == 0)
        {
            throw new Exception("���� �����Ͱ� ����ֽ��ϴ�.");
        }

        LevelNumber = levelNumber;
        _progressTime = progressTime;
    }

    private void AddLevel(StageLevel level)
    {
        if (level == null)
        {
            throw new Exception("������ null�� �� �����ϴ�.");
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
