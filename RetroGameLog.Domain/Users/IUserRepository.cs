namespace RetroGameLog.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid UserId, CancellationToken cancellationToken = default);

    Task<bool> IsEmailOverlaped(string email);

    Task<bool> IsUsernameOverlaped(string username);

    void Add(User user);
}
