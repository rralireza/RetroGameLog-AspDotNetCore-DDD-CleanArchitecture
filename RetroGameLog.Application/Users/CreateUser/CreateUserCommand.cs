using RetroGameLog.Application.Abstractions.Messaging;
using RetroGameLog.Domain.Users;

namespace RetroGameLog.Application.Users.CreateUser;

public record CreateUserCommand(string FirstName, string LastName, string Email, string Username, string Password) : ICommand<Guid>;