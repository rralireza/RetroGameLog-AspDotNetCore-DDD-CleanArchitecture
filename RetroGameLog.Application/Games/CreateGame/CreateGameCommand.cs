using RetroGameLog.Application.Abstractions.Messaging;
using RetroGameLog.Domain.Games;

namespace RetroGameLog.Application.Games.CreateGame;

public sealed record CreateGameCommand(
    string Title,
    string Platform,
    int ReleaseYear,
    string Genre,
    string Developer) : ICommand<Guid>;
