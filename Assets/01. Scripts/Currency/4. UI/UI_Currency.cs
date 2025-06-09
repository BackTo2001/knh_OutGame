using TMPro;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;

public class UI_Currency : MonoBehaviour
{
    public TextMeshProUGUI GoldCountText;
    public TextMeshProUGUI DiamondCountText;

    private void Start()
    {
        Refresh();

        CurrencyManager.Instance.OnDataChanged += Refresh;
    }

    private void Refresh()
    {
        GoldCountText.text = $"Gold: {CurrencyManager.Instance.Get(ECurrencyType.Gold).Value}";
        DiamondCountText.text = $"Diamond: {CurrencyManager.Instance.Get(ECurrencyType.Diamond).Value}";
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
        // 디미터 법칙 : 묻지 말고 시켜라
        // 현재 문제
        // 1. 도메인의 규칙이 UI에 노출되어 있다.
        // 2. 규칙이 바뀌면 UI까지 와서 수정해야한다.
        // 3. '사다'라는 행위는 '커렌시 도메인'의 중요한 역할
        // 4. '사다'라는 행위는 상점 등 다양한 곳에서 쓰일텐데.. 그때마다 로직을 ?
        if (CurrencyManager.Instance.Get(ECurrencyType.Gold).Value >= 300)
        {
            CurrencyManager.Instance.Subtract(ECurrencyType.Gold, 300);

            var player = GameObject.FindFirstObjectByType<PlayerCharacterController>();
            Health playerHealth = player.GetComponent<Health>();
            playerHealth.Heal(100);
        }
    }
}
