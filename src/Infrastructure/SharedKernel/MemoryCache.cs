using ApplicationCore.ISharedKernel;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace Infrastructure.SharedKernel
{
    public class MemoryCache : ICache
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public bool Get<T>(object key, out T value)
        {
            return _memoryCache.TryGetValue(key, out value);
        }

        public void Set(object key, object value, TimeSpan? offset = null)
        {
            if (offset.HasValue) _memoryCache.Set(key, value);
            else _memoryCache.Set(key, value, offset.Value);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}