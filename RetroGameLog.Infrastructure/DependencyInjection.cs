using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RetroGameLog.Application.Abstractions.Notification;
using RetroGameLog.Domain.Abstractions;
using RetroGameLog.Domain.Games;
using RetroGameLog.Infrastructure.DatabaseContext;
using RetroGameLog.Infrastructure.Notification;
using RetroGameLog.Infrastructure.Repositories;

namespace RetroGameLog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterToIoc(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<INotificationService, NotificationService>();

        var connectionString = configuration.GetConnectionString("DatabaseConnection") ?? throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<RetroGameLogDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IGameRepository, GameRepository>();

        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<RetroGameLogDbContext>());

        return services;
    }
}
