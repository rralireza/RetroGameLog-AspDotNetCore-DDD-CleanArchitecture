using Microsoft.Extensions.DependencyInjection;

namespace RetroGameLog.Application;

public static class DependencyInjection
{
    public static IServiceCollection StartUpConfiguration(this IServiceCollection services)
    {
        //Inject MediatR
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        return services;
    }
}
