using RetroGameLog.Application.Abstractions.Messaging;
using RetroGameLog.Domain.Games;

namespace RetroGameLog.Application.Games.CreateGame;

public sealed record CreateGameCommand(
    GameTitle Title,
    Platform Platform,
    ReleaseYear ReleaseYear,
    Genre Genre,
    Developer Developer) : ICommand<Guid>;
