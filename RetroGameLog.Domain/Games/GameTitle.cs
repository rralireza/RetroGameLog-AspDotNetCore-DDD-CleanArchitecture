namespace RetroGameLog.Domain.Games;

public sealed record GameTitle
{
    public string Value { get; }

    private GameTitle(string value)
    {
        Value = value;
    }

    public static GameTitle Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Game title cannot be empty!");

        if (value.Length > 100)
            throw new ArgumentException("Game title is too long!");

        return new GameTitle(value);
    }

    public override string ToString() => Value;
}
