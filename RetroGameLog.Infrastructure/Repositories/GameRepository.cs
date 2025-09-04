using Microsoft.EntityFrameworkCore;
using RetroGameLog.Domain.Games;
using RetroGameLog.Infrastructure.DatabaseContext;

namespace RetroGameLog.Infrastructure.Repositories;

internal sealed class GameRepository : Repository<Game>, IGameRepository
{
    public GameRepository(RetroGameLogDbContext context) : base(context)
    {

    }

    public async Task<bool> IsGameTitleExistsAsync(string title)
    {
        return await DbContext
            .Set<Game>()
            .AnyAsync(game => game.Title.Value == title);
    }
}
