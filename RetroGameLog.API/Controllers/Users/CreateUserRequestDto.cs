using RetroGameLog.Domain.Users;

namespace RetroGameLog.API.Controllers.Users;

public sealed record CreateUserRequestDto(string FirstName, string LastName, string Email, string Username, string Password);
