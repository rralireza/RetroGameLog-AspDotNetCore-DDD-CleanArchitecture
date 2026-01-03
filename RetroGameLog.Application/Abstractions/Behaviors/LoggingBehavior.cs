using MediatR;
using Microsoft.Extensions.Logging;
using RetroGameLog.Application.Abstractions.Messaging;
using RetroGameLog.Domain.Abstractions;
using Serilog.Context;

namespace RetroGameLog.Application.Abstractions.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest
    where TResponse : Result
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
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

            if (response.IsSuccess)
            {
                _logger.LogInformation($"{requestName} has completed successfully.");
            }
            else
            {
                using (LogContext.PushProperty("ErrorTitle", response.Error, true))
                {
                    _logger.LogError($"{requestName} has failed. Description: {response.Error}");
                }
            }
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError($"There's an error while executing {requestName} => {ex}");

            throw;
        }
    }
}