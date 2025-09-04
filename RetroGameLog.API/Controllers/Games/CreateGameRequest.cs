using RetroGameLog.Domain.Games;

namespace RetroGameLog.API.Controllers.Games;

public sealed record CreateGameRequest(string Title, string Platform, int ReleaseYear, string Genre, string Developer);

