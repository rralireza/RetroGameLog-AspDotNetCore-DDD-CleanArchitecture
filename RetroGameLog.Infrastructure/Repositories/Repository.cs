using Microsoft.EntityFrameworkCore;
using RetroGameLog.Domain.Abstractions;
using RetroGameLog.Infrastructure.DatabaseContext;

namespace RetroGameLog.Infrastructure.Repositories;

internal abstract class Repository<T> where T : Entity
{
    protected readonly RetroGameLogDbContext DbContext;

    protected Repository(RetroGameLogDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<T>().FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
    }

    public void Add(T entity)
    {
        DbContext.Add(entity);
    }
}
