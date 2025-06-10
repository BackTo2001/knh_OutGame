using System.Collections.Generic;
using UnityEngine;

public class UI_Achievement : MonoBehaviour
{
    public GameObject Parent;
    public GameObject SlotPrefab;
    private List<UI_AchievementSlot> _slots;

    private void Start()
    {

        Init();

        AchievementManager.Instance.OnDataChanged += Refresh;
    }

    private void Init()
    {
        List<AchievementDTO> achievements = AchievementManager.Instance.Achievements;
        _slots = new List<UI_AchievementSlot>();
        for (int i = 0; i < achievements.Count; i++)
        {
            GameObject slot = Instantiate(SlotPrefab, Parent.transform);
            _slots.Add(slot.transform.GetComponent<UI_AchievementSlot>());
        }
        Refresh();
    }

    private void Refresh()
    {
        List<AchievementDTO> achievements = AchievementManager.Instance.Achievements;

        for (int i = 0; i < achievements.Count; i++)
        {
            _slots[i].Refresh(achievements[i]);
        }
    }
}
