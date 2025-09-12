using RetroGameLog.Domain.Abstractions;
using RetroGameLog.Domain.Achivements;
using RetroGameLog.Domain.Reviews;
using RetroGameLog.Domain.Users.Events;

namespace RetroGameLog.Domain.Users;

public sealed class User : Entity
{
    private User(Guid id, FullName fullName, Email email, Username userName, DateTime registeredAt) : base(id)
    {
        FullName = fullName;
        Email = email;
        Username = userName;
        RegisteredAt = registeredAt;
    }

    private User() { }

    public FullName FullName { get; private set; }

    public Email Email { get; private set; }

    public Username Username { get; private set; }

    public DateTime RegisteredAt { get; private set; }

    private readonly List<Review> _reviews = new();

    public IReadOnlyList<Review> Reviews => _reviews.AsReadOnly();

    private readonly List<Achivement> _achivements = new();

    public IReadOnlyList<Achivement> Achivements => _achivements.AsReadOnly();

    public void AddReview(Review review)
    {

        _reviews.Add(review);
    }

    public void AddAchivement(Achivement achivement)
    {

        _achivements.Add(achivement);
    }

    public static User Create(FullName fullName, Email email, Username username, DateTime registeredAt)
    {
        if (fullName is null)
            throw new ArgumentNullException($"{nameof(FullName)} cannot be blank!");

        if (email is null)
            throw new ArgumentNullException($"{nameof(Email)} cannot be blank!");

        if (username is null)
            throw new ArgumentNullException($"{nameof(Username)} cannot be blank!");

        if (registeredAt > DateTime.UtcNow || registeredAt < DateTime.UtcNow)
            throw new ArgumentException("Registeration date is invalid!");

        var user = new User(Guid.NewGuid(), fullName, email, username, registeredAt);

        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

        return user;
    }
}
