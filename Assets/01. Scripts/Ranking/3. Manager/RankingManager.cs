using System;
using System.Collections.Generic;



public class RankingManager : MonoBehaviourSingleton<RankingManager>
{
    private RankingRepository _repository;
    private List<Ranking> _rankings;
    public List<RankingDTO> Rankings => _rankings.ConvertAll(r => r.ToDTO());

    private Ranking _myRanking;

    public RankingDTO MyRanking => _myRanking.ToDTO();

    public event Action OnDataChanged;

    protected override void Awake()
    {
        base.Awake();

        Init();
    }

    private void Init()
    {
        _repository = new RankingRepository();

        List<RankingSaveData> saveDataList = _repository.Load();

        _rankings = new List<Ranking>();
        foreach (RankingSaveData saveData in saveDataList)
        {
            Ranking ranking = new Ranking(saveData.Email, saveData.Nickname, saveData.Score);
            _rankings.Add(ranking);

            if (ranking.Email == AccountManager.Instance.CurrentAccount.Email)
            {
                _myRanking = ranking;
            }
        }

        if (_myRanking == null)
        {
            AccountDTO me = AccountManager.Instance.CurrentAccount;
            _myRanking = new Ranking(me.Email, me.Nickname, 0);

            _rankings.Add(_myRanking);
        }

        Sort();

        OnDataChanged?.Invoke();
    }

    private void Sort()
    {
        _rankings.Sort((r1, r2) => r2.Score.CompareTo(r1));

        for (int i = 0; i < _rankings.Count; i++)
        {
            _rankings[i].SetRank(i + 1);
        }
    }

    public void AddScore(int score)
    {
        _myRanking.AddScore(score);

        Sort();

        // Save

        OnDataChanged?.Invoke();
    }
}
