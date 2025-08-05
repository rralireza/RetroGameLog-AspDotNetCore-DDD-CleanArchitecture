using Dapper;
using RetroGameLog.Application.Abstractions.Data;
using RetroGameLog.Application.Abstractions.Messaging;
using RetroGameLog.Domain.Abstractions;

namespace RetroGameLog.Application.Games.GetGame;

internal sealed class GetGameQueryHandler : IQueryHandler<GetGameQuery, GameResponseDto>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetGameQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<GameResponseDto>> Handle(GetGameQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string query = $"""
                              SELECT
                                id AS Id,
                                title AS Title,
                                platform AS Platform,
                                releaseYear AS ReleaseYear,
                                genre AS Genre,
                                developer AS Developer,
                              FROM games
                              WHERE id = @gameId
                              """;

        GameResponseDto? game = await connection.QueryFirstOrDefaultAsync(query, new { request.GameId });

        return game;
    }
}