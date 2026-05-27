using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quartz;
using RetroGameLog.Application.Abstractions.Data;
using RetroGameLog.Application.Clock;
using RetroGameLog.Domain.Abstractions;
using System.Data;

namespace RetroGameLog.Infrastructure.Outbox;

//Don't process more than 1 job at same time!
[DisallowConcurrentExecution]
internal sealed class ProcessOutboxMessagesJob : IJob
{
    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };

    private readonly ISqlConnectionFactory _connection;

    private readonly IPublisher _publisher;

    private readonly IDateTimeProvider _dateTimeProvider;

    private readonly OutboxOptions _outboxOptions;

    private readonly ILogger<ProcessOutboxMessagesJob> _logger;

    public ProcessOutboxMessagesJob(ISqlConnectionFactory connection, IPublisher publisher, IDateTimeProvider dateTimeProvider, IOptions<OutboxOptions> outboxOptions, ILogger<ProcessOutboxMessagesJob> logger)
    {
        _connection = connection;
        _publisher = publisher;
        _dateTimeProvider = dateTimeProvider;
        _outboxOptions = outboxOptions.Value;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Start processing outbox messages...");

        //Connect to DB
        using var connection = _connection.CreateConnection();

        //Start transaction
        using var transaction = connection.BeginTransaction();

        //Get all outbox messages
        var outboxMesssages = await GetAllOutboxMessagesAsync(connection, transaction);

        //Process each message
        foreach (var message in outboxMesssages)
        {
            //Initial exception definition for error column
            Exception? exception = null;

            try
            {
                //Deserialize content
                var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(message.Content, JsonSerializerSettings);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured while processing outbox message - {message.Id}");

                exception = ex;
            }

            //Update outbox message status
            await UpdateOutboxMessageAsync(connection, transaction, message, exception?.ToString());
        }

        transaction.Commit();

        _logger.LogInformation("Completed processing outbox messages!");
    }

    private async Task<IReadOnlyList<OutboxMessageResponse>> GetAllOutboxMessagesAsync(IDbConnection connection, IDbTransaction transaction)
    {
        //UPDLOCK means locked for update
        //READPAST means skip locked rows

        string query = $"""
            SELECT TOP {_outboxOptions.BatchSize} id, content
            FROM OutboxMessages WITH (UPDLOCK, READPAST)
            WHERE ProcessedOnUtc IS NULL
            ORDER BY OccurredOnUtc
            """;

        var outboxMessages = await connection.QueryAsync<OutboxMessageResponse>(query, transaction: transaction);

        return outboxMessages.ToList();
    }


    private async Task UpdateOutboxMessageAsync(IDbConnection connection, IDbTransaction transaction, OutboxMessageResponse outboxMessage, string error)
    {
        const string query = @"""
                UPDATE OutboxMessages
                SET ProcessedOnUtc = @ProcessedOnUtc,
                    Error = @Error
                WHERE Id = @Id
        """;

        await connection.ExecuteAsync(query, new
        {
            outboxMessage.Id,
            ProcessedOnUtc = _dateTimeProvider.UtcNow,
            Error = error
        }, transaction: transaction);


    }
}
