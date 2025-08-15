using Microsoft.EntityFrameworkCore;
using RetroGameLog.Domain.Abstractions;

namespace RetroGameLog.Infrastructure.DatabaseContext;

public sealed class RetroGameLogDbContext : DbContext, IUnitOfWork
{
    public RetroGameLogDbContext(DbContextOptions options) : base(options)
    {
    }
}
