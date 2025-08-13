using Dapper;
using RetroGameLog.Application.Abstractions.Data;
using RetroGameLog.Application.Abstractions.Messaging;
using RetroGameLog.Domain.Abstractions;

namespace RetroGameLog.Application.Games.GetAllGames;

internal sealed class GetAllGamesQueryHandler : IQueryHandler<GetAllGamesQuery, IReadOnlyList<GameResponseDto>>
{
    private readonly ISqlConnectionFactory _connection;

    public GetAllGamesQueryHandler(ISqlConnectionFactory connection)
    {
        _connection = connection;
    }

    public async Task<Result<IReadOnlyList<GameResponseDto>>> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
    {
        using var connection = _connection.CreateConnection();

        const string query = """
                SELECT
                    id AS Id,
                    title AS Title,
                    platform AS Platform,
                    releaseYear AS ReleaseYear,
                    genre AS Genre,
                    developer AS Developer
                 FROM Games
            """;

        var games = await connection.QueryAsync<GameResponseDto>(query);

        return Result.Success<IReadOnlyList<GameResponseDto>>(games.ToList());
    }
}
