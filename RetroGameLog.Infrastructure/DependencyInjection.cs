using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RetroGameLog.Application.Abstractions.Data;
using RetroGameLog.Application.Abstractions.Notification;
using RetroGameLog.Domain.Abstractions;
using RetroGameLog.Domain.Games;
using RetroGameLog.Domain.Users;
using RetroGameLog.Infrastructure.DatabaseConnection;
using RetroGameLog.Infrastructure.DatabaseContext;
using RetroGameLog.Infrastructure.Notification;
using RetroGameLog.Infrastructure.Repositories;

namespace RetroGameLog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<INotificationService, NotificationService>();

        var connectionString = configuration.GetConnectionString("DatabaseConnection") ?? throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<RetroGameLogDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IGameRepository, GameRepository>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<RetroGameLogDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(new SqlConnectionFactory(connectionString));

        return services;
    }
}
