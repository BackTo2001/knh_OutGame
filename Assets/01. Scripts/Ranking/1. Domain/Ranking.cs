public class Ranking
{
    public int Rank { get; private set; }
    public readonly string Email;
    public readonly string Nickname;

    public int Score { get; private set; }

    public Ranking(string email, string nickname, int score)
    {
        var emailSpecification = new AccountEmailSpecification();

        if (!emailSpecification.IsSatisfiedBy(email))
        {
            throw new System.Exception(emailSpecification.ErrorMessage);
        }

        var nicknameSpecification = new AccountNicknameSpecification();
        if (!nicknameSpecification.IsSatisfiedBy(nickname))
        {
            throw new System.Exception(nicknameSpecification.ErrorMessage);
        }

        if (score < 0)
        {
            throw new System.Exception("������ 0 �̻��̾�� �մϴ�.");
        }

        Email = email;
        Nickname = nickname;
        Score = score;
    }

    public void SetRank(int rank)
    {
        if (rank <= 0)
        {
            throw new System.Exception("��ũ�� 1 �̻��̾�� �մϴ�.");
        }
        Rank = rank;
    }

    public void AddScore(int score)
    {
        if (score <= 0)
        {
            throw new System.Exception("�ùٸ��� ���� �����Դϴ�.");

        }
        Score += score;
    }

    public RankingDTO ToDTO()
    {
        return new RankingDTO(this);
    }
}
