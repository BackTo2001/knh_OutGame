using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_AchievementSlot : MonoBehaviour
{
    public TextMeshProUGUI NameTextUI;
    public TextMeshProUGUI DescriptionUI;
    public TextMeshProUGUI RewardAmountUI;
    public Slider ProgressSliderUI;
    public TextMeshProUGUI ProgressTextUI;
    public TextMeshProUGUI RewardClaimDateUI;
    public Button RewardClaimButtonUI;

    private AchievementDTO _achievementDTO;

    public void Refresh(AchievementDTO achievementDTO)
    {
        _achievementDTO = achievementDTO;

        NameTextUI.text = achievementDTO.Name;
        DescriptionUI.text = achievementDTO.Description;
        RewardAmountUI.text = achievementDTO.RewardAmount.ToString();
        ProgressSliderUI.value = (float)achievementDTO.CurrentValue / achievementDTO.GoalValue;
        ProgressTextUI.text = $"{achievementDTO.CurrentValue} / {achievementDTO.GoalValue}";

        RewardClaimButtonUI.interactable = achievementDTO.CanClaimReward();
    }

    public void ClaimReward()
    {
        if (AchievementManager.Instance.TryClaimReward(_achievementDTO))
        {
            // 보상 청구 성공
            // 꽃가루 
        }
        else
        {
            // 진행도가 부족합니다.
        }
    }
}
