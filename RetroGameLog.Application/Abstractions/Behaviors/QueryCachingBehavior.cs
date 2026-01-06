using MediatR;
using Microsoft.Extensions.Logging;
using RetroGameLog.Application.Abstractions.Caching;
using RetroGameLog.Domain.Abstractions;

namespace RetroGameLog.Application.Abstractions.Behaviors;

internal sealed class QueryCachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICachedQuery
    where TResponse : Result
{
    private readonly ICacheService _cacheService;
    private readonly ILogger<QueryCachingBehavior<TRequest, TResponse>> _logger;
    public QueryCachingBehavior(ICacheService cacheService, ILogger<QueryCachingBehavior<TRequest, TResponse>> logger)
    {
        _cacheService = cacheService;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TResponse? cachedResult = await _cacheService.GetAsync<TResponse>(request.CacheKey, cancellationToken);

        string cacheQueryName = $"{request.CacheKey} - {typeof(TRequest).Name}";

        if (cachedResult is not null)
        {
            _logger.LogInformation($"Cache hit for query {cacheQueryName}");

            return cachedResult;
        }

        _logger.LogInformation($"Cache miss for query {cacheQueryName}");

        var result = await next();

        if (result.IsSuccess)
        {
            await _cacheService.SetAsync(request.CacheKey, result, request.CacheExpiration, cancellationToken);
        }

        return result;
    }
}
