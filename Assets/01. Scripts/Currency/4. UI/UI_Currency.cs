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
        // ����� ��Ģ : ���� ���� ���Ѷ�
        // ���� ����
        // 1. �������� ��Ģ�� UI�� ����Ǿ� �ִ�.
        // 2. ��Ģ�� �ٲ�� UI���� �ͼ� �����ؾ��Ѵ�.
        // 3. '���'��� ������ 'Ŀ���� ������'�� �߿��� ����
        // 4. '���'��� ������ ���� �� �پ��� ������ �����ٵ�.. �׶����� ������ ?
        if (CurrencyManager.Instance.Get(ECurrencyType.Gold).Value >= 300)
        {
            CurrencyManager.Instance.Subtract(ECurrencyType.Gold, 300);

            var player = GameObject.FindFirstObjectByType<PlayerCharacterController>();
            Health playerHealth = player.GetComponent<Health>();
            playerHealth.Heal(100);
        }
    }
}
