namespace RetroGameLog.Domain.Achivements;

public sealed record AchivementDescription
{
    public string Value { get; }

    private AchivementDescription(string value)
    {
        Value = value;
    }

    public static AchivementDescription Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Achievement description cannot be blank!");
        }

        if (value.Length > 250)
        {
            throw new ArgumentException("Achivement description cannot be more than 250 characters!");
        }

        return new AchivementDescription(value);
    }

    public override string ToString() => Value;
}
