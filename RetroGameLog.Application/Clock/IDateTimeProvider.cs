namespace RetroGameLog.Application.Clock;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }

    DateTime Now { get; }
}
