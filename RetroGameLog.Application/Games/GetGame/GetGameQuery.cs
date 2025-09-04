using RetroGameLog.Application.Abstractions.Messaging;
using RetroGameLog.Application.Games.DTO;

namespace RetroGameLog.Application.Games.GetGame;

public sealed record GetGameQuery(Guid GameId) : IQuery<GameResponseDto>;