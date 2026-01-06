using Microsoft.Extensions.Caching.Distributed;
using RetroGameLog.Application.Abstractions.Caching;
using System.Text.Json;

namespace RetroGameLog.Infrastructure.Caching;

internal sealed class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;

    public CacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        byte[]? value = await _cache.GetAsync(key, cancellationToken);

        return value is not null ? Deserlialize<T>(value) : default;
    }

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        return _cache.RemoveAsync(key, cancellationToken);
    }

    public Task SetAsync<T>(string key, T value, TimeSpan? absoluteExpirationRelativeToNow = null, CancellationToken cancellationToken = default)
    {
        byte[] valueAsBytes = Serialize(value);

        return _cache.SetAsync(key, valueAsBytes, CacheOptions.Create(absoluteExpirationRelativeToNow), cancellationToken);
    }

    private static T Deserlialize<T>(byte[] bytes)
    {
        return JsonSerializer.Deserialize<T>(bytes)!;
    }

    private static byte[] Serialize<T>(T value)
    {
        return JsonSerializer.SerializeToUtf8Bytes(value);
    }
}
