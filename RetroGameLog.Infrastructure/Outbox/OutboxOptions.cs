namespace RetroGameLog.Infrastructure.Outbox;

internal sealed class OutboxOptions
{
    public int IntervalInSeconds { get; set; } //How long does it take to background job process?

    public int BatchSize { get; set; } //How many messages should be processed in one batch?
}
