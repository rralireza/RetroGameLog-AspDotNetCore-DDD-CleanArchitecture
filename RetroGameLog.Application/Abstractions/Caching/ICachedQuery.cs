using RetroGameLog.Application.Abstractions.Messaging;

namespace RetroGameLog.Application.Abstractions.Caching;

public interface ICacheable<TResponse> : IQuery<TResponse>, ICachedQuery;

public interface ICachedQuery
{
    string CacheKey { get; }

    TimeSpan? CacheExpiration { get; }
}
