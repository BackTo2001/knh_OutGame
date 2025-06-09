using TMPro;
using UnityEngine;

public class UI_Currency : MonoBehaviour
{
    public TextMeshProUGUI GoldCountText;
    public TextMeshProUGUI DiamondCountText;

    private void Update()
    {
        Refresh();
    }

    private void Refresh()
    {
        GoldCountText.text = $"Gold: {CurrencyManager.Instance.Currencies[ECurrencyType.Gold].Value}";
        DiamondCountText.text = $"Diamond: {CurrencyManager.Instance.Currencies[ECurrencyType.Diamond].Value}";
    }
}
