namespace RetroGameLog.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid UserId, CancellationToken cancellationToken = default);

    Task<bool> IsEmailOverlaped(Email email);

    Task<bool> IsUsernameOverlaped(Username username);

    void Add(User user);
}
