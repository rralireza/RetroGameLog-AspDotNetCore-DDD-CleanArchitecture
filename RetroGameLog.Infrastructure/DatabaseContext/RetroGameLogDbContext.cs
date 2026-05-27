using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RetroGameLog.Application.Clock;
using RetroGameLog.Application.Exceptions;
using RetroGameLog.Domain.Abstractions;
using RetroGameLog.Infrastructure.Outbox;

namespace RetroGameLog.Infrastructure.DatabaseContext;

public sealed class RetroGameLogDbContext : DbContext, IUnitOfWork
{
    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };


    private readonly IDateTimeProvider _dateTimeProvider;

    public RetroGameLogDbContext(DbContextOptions options, IDateTimeProvider dateTimeProvider) : base(options)
    {
        _dateTimeProvider = dateTimeProvider;
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
            PublishDomainEvenetsAsOutboxMessages();

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("A Concurrency error occurred while saving changes to the database!", ex);
        }
    }

    public void PublishDomainEvenetsAsOutboxMessages()
    {
        var outboxMessages = ChangeTracker
            .Entries<Entity>()
            .Select(x => x.Entity)
            .SelectMany(x =>
            {
                var domainEvents = x.DomainEvents;

                x.ClearDomainEvents();

                return domainEvents;
            })
            .Select(x => new OutboxMessage(Guid.NewGuid(), _dateTimeProvider.UtcNow, x.GetType().Name, JsonConvert.SerializeObject(x, JsonSerializerSettings))) //Convert domain events into outbox message type
            .ToList();

        AddRange(outboxMessages);
    }
}
