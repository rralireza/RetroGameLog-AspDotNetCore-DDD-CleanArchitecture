using HealthChecks.UI.Client;
using RetroGameLog.API.Extensions;
using RetroGameLog.Application;
using RetroGameLog.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder
    .Host
    .UseSerilog((context, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseRequestContextLogging();

app.UseSerilogRequestLogging();

app.UseAuthentication();

app.UseAuthorization();

app.UseCustomExceptionHandling();

app.MapControllers();

app.MapHealthChecks("healthStatus", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
