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
        AchievementText.text = $"���� �޼�: {achievementDTO.Name}";
        // �ڵ����� �˸��� ����� �ڷ�ƾ ����
        StartCoroutine(HideAfterDelay(5f)); // 5�� �Ŀ� ����
    }

    private System.Collections.IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        AchievementNotification.SetActive(false);
    }

}
