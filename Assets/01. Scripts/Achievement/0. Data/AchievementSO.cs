using UnityEngine;

// ��Ÿ�ӽ� ������ �ʴ� ���� SO�� �����ϸ�
// - ��ȹ�ڰ� �����Ϳ��� ���� ������ �����ϴ�.
// - ���������� Ȯ�强�� �����Ѵ�.
// - ������ ��ü(Acievement)�� ����(CurrentValue, RewardClaimed)�� �����ϸ� �ȴ�.


[CreateAssetMenu(fileName = "AchievementSO", menuName = "Scriptable Objects/AchievementSO")]
public class AchievementSO : ScriptableObject
{
    [Header("�⺻ ����")]
    [SerializeField]
    private string _id;
    public string ID => _id;

    [SerializeField]
    private string _name;
    public string Name => _name;

    [SerializeField]
    private string _description;
    public string Description => _description;


    [Header("���� ����")]
    [SerializeField]
    private EAchievementCondition _condition;
    public EAchievementCondition Condition => _condition;

    [SerializeField]
    private int _goalValue;
    public int GoalValue => _goalValue;

    [Header("���� ����")]
    [SerializeField]
    private ECurrencyType _rewardCurrencyType;
    public ECurrencyType RewardCurrencyType => _rewardCurrencyType;

    [SerializeField]
    private int _rewardAmount;
    public int RewardAmount => _rewardAmount;
}
