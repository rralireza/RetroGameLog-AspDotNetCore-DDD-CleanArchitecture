using RetroGameLog.API.Extensions;
using RetroGameLog.Application;
using RetroGameLog.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


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

app.UseCustomExceptionHandling();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
