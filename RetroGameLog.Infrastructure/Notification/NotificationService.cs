using RetroGameLog.Application.Abstractions.Notification;

namespace RetroGameLog.Infrastructure.Notification;

internal sealed class NotificationService : INotificationService
{
    public Task SendAsync(string status, string message)
    {
        return Task.CompletedTask; // No-op implementation
    }
}
