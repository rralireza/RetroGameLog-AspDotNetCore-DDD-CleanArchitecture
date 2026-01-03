using Microsoft.EntityFrameworkCore;
using RetroGameLog.API.Middleware;
using RetroGameLog.Infrastructure.DatabaseContext;

namespace RetroGameLog.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var database = scope.ServiceProvider.GetRequiredService<RetroGameLogDbContext>();

        //database.Database.Migrate();
    }

    public static void UseCustomExceptionHandling(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }

    public static void UseRequestContextLogging(this IApplicationBuilder app)
    {
        app.UseMiddleware<RequestContextLoggingMiddleware>();
    }
}
