using RetroGameLog.Domain.Abstractions;

namespace RetroGameLog.Domain.Reviews.Events;

public sealed record ReviewCreatedDomainEvent(Guid ReviewId) : IDomainEvent;
