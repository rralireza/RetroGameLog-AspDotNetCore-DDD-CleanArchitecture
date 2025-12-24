using RetroGameLog.Domain.Users;

namespace RetroGameLog.API.Controllers.Users;

public sealed record CreateUserRequestDto(string FirstName, string LastName, Email Email, Username Username, string Password, DateTime RegisteredAt);
