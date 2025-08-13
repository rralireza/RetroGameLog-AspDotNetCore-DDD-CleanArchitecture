using RetroGameLog.Application.Abstractions.Messaging;
using RetroGameLog.Domain.Users;

namespace RetroGameLog.Application.Users.CreateUser;

public record CreateUserCommand(string FirstName, string LastName, Email Email, Username Username, DateTime RegisteredAt) : ICommand<Guid>;