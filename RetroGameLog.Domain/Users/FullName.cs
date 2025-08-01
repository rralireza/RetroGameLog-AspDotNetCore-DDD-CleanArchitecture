namespace RetroGameLog.Domain.Users;

public record FullName
{
    public string FirstName { get; }

    public string LastName { get; }

    private FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static FullName Create(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be blank!");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be blank!");

        if (firstName.Length > 50)
            throw new ApplicationException("First name cannot be more than 50 characters!");

        if (lastName.Length > 70)
            throw new ApplicationException("Last name cannot be more than 70 characters!");

        return new FullName(firstName, lastName);
    }
}
