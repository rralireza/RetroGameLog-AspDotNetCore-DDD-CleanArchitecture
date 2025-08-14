using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RetroGameLog.Application.Abstractions.Notification;
using RetroGameLog.Infrastructure.Notification;

namespace RetroGameLog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterToIoc(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<INotificationService, NotificationService>();

        return services;
    }
}
