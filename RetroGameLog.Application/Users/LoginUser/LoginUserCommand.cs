using RetroGameLog.Application.Abstractions.Messaging;

namespace RetroGameLog.Application.Users.LoginUser;

public sealed record LoginUserCommand(string Email, string Password) : ICommand<AccessTokenResponse>;
