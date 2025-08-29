using MediatR;
using Microsoft.EntityFrameworkCore;
using RetroGameLog.Application.Exceptions;
using RetroGameLog.Domain.Abstractions;

namespace RetroGameLog.Infrastructure.DatabaseContext;

public sealed class RetroGameLogDbContext : DbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;

    public RetroGameLogDbContext(DbContextOptions options, IPublisher publisher) : base(options)
    {
        _publisher = publisher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RetroGameLogDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            await PublishDomainEvenets();

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("A Concurrency error occurred while saving changes to the database!", ex);
        }
    }

    public async Task PublishDomainEvenets()
    {
        var domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(x => x.Entity)
            .SelectMany(x =>
            {
                var domainEvents = x.DomainEvents;

                x.ClearDomainEvents();

                return domainEvents;
            }).ToList();

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }
}
