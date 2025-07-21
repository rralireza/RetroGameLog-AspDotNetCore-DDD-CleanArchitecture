namespace RetroGameLog.Domain.Achivements;

public sealed record AchivementTitle
{
    public string Value { get; }

    private AchivementTitle(string value)
    {
        Value = value;
    }

    public static AchivementTitle Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Achievement title cannot be blank!");
        }

        if (value.Length > 100)
        {
            throw new ArgumentException("Achievement title cannot exceed 100 characters.");
        }

        if (!char.IsUpper(value[0]))
        {
            throw new ArgumentException("Achievement title must start with an uppercase letter.");
        }

        return new AchivementTitle(value);
    }

    public override string ToString() => Value;
}
