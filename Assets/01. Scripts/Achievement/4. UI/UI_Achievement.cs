using System.Collections.Generic;
using UnityEngine;

public class UI_Achievement : MonoBehaviour
{
    [SerializeField]
    private List<UI_AchievementSlot> _slots;

    private void OnEnable()
    {
        AchievementManager.Instance.OnDataChanged += Refresh;
        Refresh();
    }

    private void OnDisable()
    {
        if (AchievementManager.Instance != null)
            AchievementManager.Instance.OnDataChanged -= Refresh;
    }

    private void Refresh()
    {
        List<AchievementDTO> achievements = AchievementManager.Instance.Achievements();

        for (int i = 0; i < achievements.Count && i < _slots.Count; i++)
        {
            _slots[i].Refresh(achievements[i]);
        }
    }
}
