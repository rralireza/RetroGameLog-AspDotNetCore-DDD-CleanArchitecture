using RetroGameLog.Application.Abstractions.Messaging;
using RetroGameLog.Application.Games.DTO;

namespace RetroGameLog.Application.Games.GetAllGames;

public sealed record GetAllGamesQuery() : IQuery<IReadOnlyList<GameResponseDto>>;
