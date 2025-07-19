namespace RetroGameLog.Domain.Games;

public sealed record GameTitle
{
    public string Value { get; }

    public GameTitle(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Game title cannot be empty!");

        if (value.Length > 100)
            throw new ArgumentException("Game title is too long!");

        Value = value;
    }

    public override string ToString() => Value;
}
