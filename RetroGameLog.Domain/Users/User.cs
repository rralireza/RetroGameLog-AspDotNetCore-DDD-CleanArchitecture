using RetroGameLog.Domain.Abstractions;
using RetroGameLog.Domain.Achivements;
using RetroGameLog.Domain.Reviews;
using RetroGameLog.Domain.Users.Events;
using System.ComponentModel.DataAnnotations;

namespace RetroGameLog.Domain.Users;

public sealed class User : Entity
{
    private User(Guid id, FullName fullName, Email email, Username userName, List<Role> userRoles) : base(id)
    {
        FullName = fullName;
        Email = email;
        Username = userName;
        RegisteredAt = DateTime.UtcNow;
        _roles = userRoles;
    }

    private User() { }

    public FullName FullName { get; private set; }

    public Email Email { get; private set; }

    public Username Username { get; private set; }

    public DateTime RegisteredAt { get; private set; } = DateTime.UtcNow;

    public string IdentityId { get; private set; } = string.Empty;

    private readonly List<Review> _reviews = new();

    public IReadOnlyList<Review> Reviews => _reviews.AsReadOnly();

    private readonly List<Achivement> _achivements = new();

    public IReadOnlyList<Achivement> Achivements => _achivements.AsReadOnly();

    private readonly List<Role> _roles = new();

    public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();

    public void AddReview(Review review)
    {

        _reviews.Add(review);
    }

    public void AddAchivement(Achivement achivement)
    {

        _achivements.Add(achivement);
    }

    public void SetIdentityId(string identityId)
    {
        if (string.IsNullOrWhiteSpace(identityId))
            throw new ArgumentNullException("Identity ID is null");

        IdentityId = identityId;
    }

    public static User Create(FullName fullName, Email email, Username username, List<Role> roles)
    {
        if (fullName is null)
            throw new ArgumentNullException($"{nameof(FullName)} cannot be blank!");

        if (email is null)
            throw new ArgumentNullException($"{nameof(Email)} cannot be blank!");

        if (username is null)
            throw new ArgumentNullException($"{nameof(Username)} cannot be blank!");

        var user = new User(Guid.NewGuid(), fullName, email, username, roles);

        if (!roles.Any())
        {
            user._roles.Add(Role.User);
        }

        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

        return user;
    }
}
