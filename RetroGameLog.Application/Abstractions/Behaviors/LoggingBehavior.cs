using MediatR;
using Microsoft.Extensions.Logging;
using RetroGameLog.Application.Abstractions.Messaging;

namespace RetroGameLog.Application.Abstractions.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseCommand
{
    private readonly ILogger<TRequest> _logger;

    public LoggingBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = request.GetType().Name;

        try
        {
            _logger.LogInformation($"Executing {requestName}...");

            var response = await next();
            
            _logger.LogInformation($"{requestName} has completed successfully.");
            
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError($"There's an error while executing {requestName} => {ex}");

            throw;
        }
    }
}