public class RankingDTO
{
    public readonly int Rank;
    public readonly string Email;
    public readonly string Nickname;
    public readonly int Score;

    public RankingDTO(Ranking ranking)
    {
        Rank = ranking.Rank;
        Email = ranking.Email;
        Nickname = ranking.Nickname;
        Score = ranking.Score;
    }
}
