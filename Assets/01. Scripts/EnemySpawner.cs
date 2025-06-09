using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPoolType
{
    Enemy_HoverBot
}

public class EnemySpawner : MonoBehaviour
{
    [Header("스폰 설정")]
    public Transform[] spawnPoints;
    public float spawnInterval = 3f;
    public int maxEnemies = 10;

    [Header("풀링 타입")]
    public EPoolType enemyPoolType = EPoolType.Enemy_HoverBot;

    private List<GameObject> activeEnemies = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            CleanupNullEnemies();

            if (activeEnemies.Count >= maxEnemies)
                continue;

            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemy = ObjectPoolManager.Instance.Spawn(enemyPoolType, spawnPoint.position, spawnPoint.rotation);
            activeEnemies.Add(enemy);
        }
    }

    private void CleanupNullEnemies()
    {
        activeEnemies.RemoveAll(enemy => enemy == null || !enemy.activeInHierarchy);
    }
}
