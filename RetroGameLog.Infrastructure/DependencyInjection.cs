
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RetroGameLog.Application.Abstractions.Authentication;
using RetroGameLog.Application.Abstractions.Caching;
using RetroGameLog.Application.Abstractions.Data;
using RetroGameLog.Application.Abstractions.Notification;
using RetroGameLog.Domain.Abstractions;
using RetroGameLog.Domain.Games;
using RetroGameLog.Domain.Users;
using RetroGameLog.Infrastructure.Authentication;
using RetroGameLog.Infrastructure.Authorization;
using RetroGameLog.Infrastructure.Caching;
using RetroGameLog.Infrastructure.DatabaseConnection;
using RetroGameLog.Infrastructure.DatabaseContext;
using RetroGameLog.Infrastructure.Notification;
using RetroGameLog.Infrastructure.Repositories;
using AuthenticationOptions = RetroGameLog.Infrastructure.Authentication.AuthenticationOptions;
using AuthenticationService = RetroGameLog.Infrastructure.Authentication.AuthenticationService;
using IAuthenticationService = RetroGameLog.Application.Abstractions.Authentication.IAuthenticationService;

namespace RetroGameLog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<INotificationService, NotificationService>();

        AddAuthentication(services, configuration);

        AddPresistence(services, configuration);

        AddAuthorization(services);
        
        AddCaching(services, configuration);

        return services;
    }

    private static void AddCaching(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Cache") ?? throw new ApplicationException(nameof(configuration));

        services.AddStackExchangeRedisCache(options => options.Configuration = connectionString);

        services.AddSingleton<ICacheService, CacheService>();
    }

    private static void AddAuthorization(IServiceCollection services)
    {
        services.AddScoped<AuthorizationService>();

        services.AddTransient<IClaimsTransformation, CustomClaimTransformation>();

        services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();

        services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
    }

    private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        services
                    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer();

        services.Configure<AuthenticationOptions>(configuration.GetSection("Authentication"));

        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services.Configure<KeyCloakOptions>(configuration.GetSection("KeyCloak"));

        services.AddTransient<AdminAuthorizationDelegationHandler>();

        services.AddHttpClient<IAuthenticationService, AuthenticationService>((sp, httpClient) =>
        {
            var keyCloakOptions = sp.GetRequiredService<IOptions<KeyCloakOptions>>().Value;

            httpClient.BaseAddress = new Uri(keyCloakOptions.AdminUrl);
        }).AddHttpMessageHandler<AdminAuthorizationDelegationHandler>();

        services.AddHttpClient<IJwtService, JwtService>((sp, client) =>
        {
            var keyCloakOptions = sp.GetRequiredService<IOptions<KeyCloakOptions>>().Value;

            client.BaseAddress = new Uri(keyCloakOptions.TokenUrl);
        });
    }

    private static void AddPresistence(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DatabaseConnection") ?? throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<RetroGameLogDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IGameRepository, GameRepository>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<RetroGameLogDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(new SqlConnectionFactory(connectionString));
    }
}
