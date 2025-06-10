using TMPro;
using UnityEngine;

public class UI_AchievementNotification : MonoBehaviour
{
    public GameObject AchievementNotification;
    public TextMeshProUGUI AchievementText;

    public void Start()
    {
        AchievementManager.Instance.OnNewAchievementRewarded += Show;
    }

    public void Show(AchievementDTO achievementDTO)
    {
        if (AchievementNotification == null || AchievementText == null)
        {
            Debug.LogError("AchievementNotification or AchievementText is not assigned.");
            return;
        }
        AchievementNotification.SetActive(true);
        AchievementText.text = $"업적 달성: {achievementDTO.Name}";
        // 자동으로 알림을 숨기는 코루틴 시작
        StartCoroutine(HideAfterDelay(5f)); // 5초 후에 숨김
    }

    private System.Collections.IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        AchievementNotification.SetActive(false);
    }

}
