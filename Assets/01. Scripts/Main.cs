using UnityEngine;

public class Main : MonoBehaviour
{
    private void Start()
    {
        // ������(������) : �ذ��ϰ��� �ϴ� ���� ���� or ���� ��ü�� �ǹ�
        // ������ ��(�𵨸�) : �����ΰ� �� ��Ģ�� �߻�ȭ�� ��
        // int gold = 100;
        // int diamond = 34;

        Currency gold = new Currency(ECurrencyType.Gold, 100);
        Currency diamond = new Currency(ECurrencyType.Diamond, 100);

        //gold.Add(-700);
    }
}
