using RetroGameLog.Application.Abstractions.Messaging;

namespace RetroGameLog.Application.Games.GetAllGames;

public sealed record GetAllGamesQuery() : IQuery<IReadOnlyList<GameResponseDto>>;
