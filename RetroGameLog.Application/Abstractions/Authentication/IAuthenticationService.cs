using RetroGameLog.Domain.Users;

namespace RetroGameLog.Application.Abstractions.Authentication;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(User user, string password, CancellationToken cancellationToken = default);
}
