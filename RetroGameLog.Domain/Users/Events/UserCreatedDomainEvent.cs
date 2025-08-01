using RetroGameLog.Domain.Abstractions;

namespace RetroGameLog.Domain.Users.Events;

public sealed record UserCreatedDomainEvent(Guid UserId) : IDomainEvent;
