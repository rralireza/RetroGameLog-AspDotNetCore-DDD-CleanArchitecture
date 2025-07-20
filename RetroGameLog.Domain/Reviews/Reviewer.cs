namespace RetroGameLog.Domain.Reviews;

public sealed record Reviewer
{
    public string Username { get; }

    private Reviewer(string username)
    {
        Username = username;
    }

    public static Reviewer SetReviewer(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentNullException("Username can't be blank!");

        return new Reviewer(username);
    }

    public override string ToString() => Username;
}
