using RetroGameLog.Domain.Abstractions;

namespace RetroGameLog.Domain.Achivements.Events;

public sealed record AchivementCreatedDomainEvent(Guid AchivementId) : IDomainEvent;