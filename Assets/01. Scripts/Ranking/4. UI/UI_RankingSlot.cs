using TMPro;
using UnityEngine;

public class UI_RankingSlot : MonoBehaviour
{
    public TextMeshProUGUI RankTextUI;
    public TextMeshProUGUI NicknameTextUI;
    public TextMeshProUGUI ScoreTextUI;

    public void Refresh(RankingDTO ranking)
    {
        RankTextUI.text = ranking.Rank.ToString("N0");
        NicknameTextUI.text = ranking.Nickname;
        ScoreTextUI.text = ranking.Score.ToString("N0");
    }
}
