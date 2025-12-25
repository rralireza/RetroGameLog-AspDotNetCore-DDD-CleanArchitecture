using RetroGameLog.Domain.Abstractions;

namespace RetroGameLog.Application.Abstractions.Authentication;

public interface IJwtService
{
    Task<Result<string>> GenerateTokenAsync(string email, string password, CancellationToken cancellationToken = default);
}
