using Microsoft.Extensions.DependencyInjection;
using RetroGameLog.Application.Abstractions.Behaviors;

namespace RetroGameLog.Application;

public static class DependencyInjection
{
    public static IServiceCollection StartUpConfiguration(this IServiceCollection services)
    {
        //Inject MediatR
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        return services;
    }
}
