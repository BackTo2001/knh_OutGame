using UnityEngine;

[CreateAssetMenu(fileName = "StageLevelSO", menuName = "Scriptable Objects/StageLevelSO")]
public class StageLevelSO : ScriptableObject
{
    [Header("스테이지 레벨 이름")]
    [SerializeField] private string _name;
    public string Name => _name;

    [Header("스테이지 시작 및 종료 레벨")]
    [SerializeField] private int _startLevel;
    public int StartLevel => _startLevel;

    [SerializeField] private int _endLevel;
    public int EndLevel => _endLevel;

    [Header("스탯 배율")]
    [SerializeField] private float _healthFactor;
    public float HealthFactor => _healthFactor;

    [SerializeField] private float _damageFactor;
    public float DamageFactor => _damageFactor;

    [Header("스폰 정보")]
    [SerializeField] private float _spawnInterval;
    public float SpawnInterval => _spawnInterval;

    [SerializeField] private float _spawnRate;
    public float SpawnRate => _spawnRate;
}