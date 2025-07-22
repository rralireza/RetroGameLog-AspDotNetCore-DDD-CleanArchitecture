using RetroGameLog.Domain.Abstractions;

namespace RetroGameLog.Domain.Games.Events;

public sealed record GameCreatedDomainEvent(Guid GameId) : IDomainEvent;
