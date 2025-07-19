namespace RetroGameLog.Domain.Games;

public sealed record ReleaseYear
{
    public int Value { get; }

    public ReleaseYear(int value)
    {
        Value = value;
    }

    public static ReleaseYear Create(int value)
    {
        if (value < 1970)
            throw new ArgumentOutOfRangeException($"{nameof(value)} is invalid year!");

        return new ReleaseYear(value);
    }

    public override string ToString() => Value.ToString();
}
