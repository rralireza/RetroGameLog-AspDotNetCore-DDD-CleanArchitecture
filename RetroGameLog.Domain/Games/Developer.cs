namespace RetroGameLog.Domain.Games;

public sealed record Developer
{
    public string Value { get; }

    private Developer(string value)
    {
        Value = value;
    }

    public static Developer Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException("Developer can't be null!");

        if (value.Length > 50)
            throw new ArgumentOutOfRangeException("Developer length can't be more than 50 characters!");

        return new Developer(value);
    }

    public override string ToString() => Value;
}
