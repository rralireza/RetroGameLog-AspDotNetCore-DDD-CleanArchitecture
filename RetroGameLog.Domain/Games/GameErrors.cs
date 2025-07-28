using RetroGameLog.Domain.Abstractions;

namespace RetroGameLog.Domain.Games;

public static class GameErrors
{
    public static readonly Error NotFound = new("Games.NotFound", "Game not found");
    public static readonly Error DuplicateTitle = new("Games.DuplicateTitle", "Game title already taken!");
}
