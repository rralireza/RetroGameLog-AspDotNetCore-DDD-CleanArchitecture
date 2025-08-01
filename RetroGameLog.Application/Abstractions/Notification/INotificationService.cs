namespace RetroGameLog.Application.Abstractions.Notification;

public interface INotificationService
{
    Task SendAsync(string status, string message);
}
