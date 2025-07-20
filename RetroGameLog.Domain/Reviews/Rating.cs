namespace RetroGameLog.Domain.Reviews;

public sealed record Rating
{
    public int Score { get; }

    private Rating(int score)
    {
        Score = score;
    }

    public static Rating SetReviewScore(int score)
    {
        if (score < 1 || score > 10)
            throw new ArgumentOutOfRangeException("Review Score must be one of the numbers between 1 to 10!");

        return new Rating(score);
    }

    public override string ToString() => Score.ToString();
}
