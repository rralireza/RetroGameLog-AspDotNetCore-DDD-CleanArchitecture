using Microsoft.EntityFrameworkCore;
using RetroGameLog.Infrastructure.DatabaseContext;

namespace RetroGameLog.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var database = scope.ServiceProvider.GetRequiredService<RetroGameLogDbContext>();

        database.Database.Migrate();
    }
}
