using RetroGameLog.Application.Abstractions.Messaging;
using RetroGameLog.Domain.Users;

namespace RetroGameLog.Application.Users.CreateUser;

public record CreateUserCommand(string firstName, string lastName, Email email, Username username, DateTime registeredAt) : ICommand<Guid>;