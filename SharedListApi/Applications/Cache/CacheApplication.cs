using Microsoft.Extensions.Caching.Memory;
using System;

namespace SharedListApi.Applications.Cache
{
    public class CacheApplication : ICacheApplication
    {
        private const int ExpireSeconds = 3;
        private IMemoryCache _cache;

        public CacheApplication (IMemoryCache cache)
        {
            _cache = cache;
        }

        public void Add<T>(string cacheKey, T val)
        {
            _cache.Set<T>(cacheKey, val, DateTimeOffset.Now.AddSeconds(ExpireSeconds));
        }

        public T Get<T>(string cacheKey)
        {
            return _cache.Get<T>(cacheKey);
        }
    }
}
