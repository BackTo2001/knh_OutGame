public class StageLevel
{
    // ��ȹ ������
    public readonly string Name;
    public readonly int StartLevel;             // ���� ����
    public readonly int EndLevel;               // ���� ����
    public float HealthFactor;                  // ü�� ����
    public float DamageFactor;                  // ������ ����
    public const float Duration = 60f;          // ���� �ð�
    public readonly float SpawnInterval;        // ���� �ֱ�
    public readonly float SpwanRate;            // ���� Ȯ��

    // ���� ������
    public int CurrentLevel; // StartLevel ~ EndLevel

    public StageLevel(int startLevel, int endLevel, float healthFactor, float damageFactor, float spawnInterval, float spawnRate)
    {
        if (startLevel < 0 || endLevel < startLevel)
        {
            throw new System.Exception("���� ������ �߸��Ǿ����ϴ�.");
        }

        if (endLevel < 0 || endLevel < StartLevel)
        {
            throw new System.Exception("���� ������ 0 �̻��̾�� �մϴ�.");
        }

        if (healthFactor < 1)
        {
            throw new System.Exception("ü�� ������ 0���� Ŀ�� �մϴ�.");
        }

        if (damageFactor < 1)
        {
            throw new System.Exception("������ ������ 0���� Ŀ�� �մϴ�.");
        }

        if (spawnInterval <= 0)
        {
            throw new System.Exception("���� �ֱ�� 0���� Ŀ�� �մϴ�.");
        }

        if (spawnRate < 0 || spawnRate > 100)
        {
            throw new System.Exception("���� Ȯ���� 0�� 100 ���̿��� �մϴ�.");
        }

        if (CurrentLevel < StartLevel || CurrentLevel > EndLevel)
        {

            throw new System.Exception("���� ������ ���� ������ ���� ���� ���̿��� �մϴ�.");
        }

        StartLevel = startLevel;
        EndLevel = endLevel;
        HealthFactor = healthFactor;
        DamageFactor = damageFactor;
        SpawnInterval = spawnInterval;
        SpwanRate = spawnRate;
        CurrentLevel = StartLevel; // �ʱ� ���� ����
    }


    public bool TryLevelUp(float progressTime)
    {
        if (progressTime >= Duration)
        {
            CurrentLevel += 1;
            return true;
        }

        return false;
    }

    public bool IsClear()
    {
        return CurrentLevel == EndLevel;
    }
}
