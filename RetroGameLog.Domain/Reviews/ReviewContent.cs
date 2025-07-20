namespace RetroGameLog.Domain.Reviews;

public sealed record ReviewContent
{

    public string Value { get; }

    private ReviewContent(string value)
    {
        Value = value;
    }

    public static ReviewContent CreateContent(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException("Review content can't be blank!");

        if (value.Length > 1000)
            throw new ArgumentOutOfRangeException("Review content can't be more than 1000 characters!");

        return new ReviewContent(value);
    }

    public override string ToString() => Value;
}
