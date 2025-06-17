public class StageLevel
{
    // 기획 데이터
    public readonly string Name;
    public readonly int StartLevel;             // 시작 레벨
    public readonly int EndLevel;               // 종료 레벨
    public float HealthFactor;                  // 체력 배율
    public float DamageFactor;                  // 데미지 배율
    public const float Duration = 60f;          // 기준 시간
    public readonly float SpawnInterval;        // 스폰 주기
    public readonly float SpwanRate;            // 스폰 확률

    // 상태 데이터
    public int CurrentLevel; // StartLevel ~ EndLevel

    public StageLevel(int startLevel, int endLevel, float healthFactor, float damageFactor, float spawnInterval, float spawnRate)
    {
        if (startLevel < 0 || endLevel < startLevel)
        {
            throw new System.Exception("레벨 범위가 잘못되었습니다.");
        }

        if (endLevel < 0 || endLevel < StartLevel)
        {
            throw new System.Exception("종료 레벨은 0 이상이어야 합니다.");
        }

        if (healthFactor < 1)
        {
            throw new System.Exception("체력 배율은 0보다 커야 합니다.");
        }

        if (damageFactor < 1)
        {
            throw new System.Exception("데미지 배율은 0보다 커야 합니다.");
        }

        if (spawnInterval <= 0)
        {
            throw new System.Exception("스폰 주기는 0보다 커야 합니다.");
        }

        if (spawnRate < 0 || spawnRate > 100)
        {
            throw new System.Exception("스폰 확률은 0과 100 사이여야 합니다.");
        }

        if (CurrentLevel < StartLevel || CurrentLevel > EndLevel)
        {

            throw new System.Exception("현재 레벨은 시작 레벨과 종료 레벨 사이여야 합니다.");
        }

        StartLevel = startLevel;
        EndLevel = endLevel;
        HealthFactor = healthFactor;
        DamageFactor = damageFactor;
        SpawnInterval = spawnInterval;
        SpwanRate = spawnRate;
        CurrentLevel = StartLevel; // 초기 레벨 설정
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
