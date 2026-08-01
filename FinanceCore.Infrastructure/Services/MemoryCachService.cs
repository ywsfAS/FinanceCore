
using FinanceCore.Application.Abstractions;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace FinanceCore.Infrastructure.Services
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly ConcurrentDictionary<string, int> _versions = new();
        private readonly TimeSpan DefaultTtl = TimeSpan.FromMinutes(5);

        public MemoryCacheService(IMemoryCache cache) => _cache = cache;

        public Task<T> GetOrCreateAsync<T>(string tag , string key , Func<Task<T>> factory , TimeSpan? ttl = null)
        {
            int version = _versions.GetOrAdd(tag, 1);
           string fullKey = $"{tag}:{version}:{key}";

            return _cache.GetOrCreateAsync(fullKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = ttl ?? DefaultTtl;
                return factory();
            });

        }
        public Task InvalidateTagAsync(string tag)
        {
            _versions.AddOrUpdate(tag, 2, (_, v) => v + 1);
            return Task.CompletedTask;
        }
        public Task InvalidateKey(string key)
        {
            _cache.Remove(key);
            return Task.CompletedTask;
        }
    }
}
