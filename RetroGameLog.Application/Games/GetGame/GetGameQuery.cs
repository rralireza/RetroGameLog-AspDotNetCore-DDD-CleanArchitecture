using RetroGameLog.Application.Abstractions.Messaging;

namespace RetroGameLog.Application.Games.GetGame;

public sealed record GetGameQuery(Guid GameId) : IQuery<GameResponseDto>;