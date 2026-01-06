using Microsoft.Extensions.Caching.Distributed;

namespace RetroGameLog.Infrastructure.Caching;

public static class CacheOptions
{
    /// <summary>
    /// 1 minutes
    /// </summary>
    public static DistributedCacheEntryOptions DefaultExpiration => new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
    };

    public static DistributedCacheEntryOptions NoExpiration => new()
    {
        AbsoluteExpirationRelativeToNow = null
    };

    /// <summary>
    /// 30 seconds
    /// </summary>
    public static DistributedCacheEntryOptions ShortExpiration => new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
    };

    /// <summary>
    /// 5 minutes
    /// </summary>
    public static DistributedCacheEntryOptions MediumExpiration => new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
    };

    /// <summary>
    /// 1 hours
    /// </summary>
    public static DistributedCacheEntryOptions LongExpiration => new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
    };

    public static DistributedCacheEntryOptions Create(TimeSpan? expiration) => expiration is null ? DefaultExpiration : new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration
        };
}
