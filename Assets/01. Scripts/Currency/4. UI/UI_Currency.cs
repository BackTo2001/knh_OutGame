using TMPro;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;

public class UI_Currency : MonoBehaviour
{
    public TextMeshProUGUI GoldCountText;
    public TextMeshProUGUI DiamondCountText;
    public TextMeshProUGUI BuyHealthText;

    private void Start()
    {
        Refresh();

        CurrencyManager.Instance.OnDataChanged += Refresh;
    }

    private void Refresh()
    {
        var gold = CurrencyManager.Instance.Get(ECurrencyType.Gold);
        var diamond = CurrencyManager.Instance.Get(ECurrencyType.Diamond);

        GoldCountText.text = $"Gold: {gold.Value}";
        DiamondCountText.text = $"Diamond: {diamond.Value}";

        BuyHealthText.color = gold.HaveEnough(300) ? Color.green : Color.red;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            BuyHealth();
        }
    }

    public void BuyHealth()
    {
        if (CurrencyManager.Instance.TryBuy(ECurrencyType.Gold, 300))
        {
            var player = GameObject.FindFirstObjectByType<PlayerCharacterController>();
            Health playerHealth = player.GetComponent<Health>();
            playerHealth.Heal(100);

            // 성공 파티클 추가
        }
        else
        {
            // 알림 추가
            // 토스트 메시지 추가
        }
    }
}
