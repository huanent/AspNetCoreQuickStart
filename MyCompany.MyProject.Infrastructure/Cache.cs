using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace MyCompany.MyProject.Infrastructure
{
    public class Cache : ICache
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IJson _json;

        public Cache(IDistributedCache distributedCache, IJson json)
        {
            _distributedCache = distributedCache;
            _json = json;
        }

        public async Task RemoveAsync(object key, CancellationToken token = default(CancellationToken))
        {
            await _distributedCache.RemoveAsync(key.ToString(), token);
        }

        public async Task SetAsync(object key, object value, TimeSpan? exp = null, CancellationToken token = default(CancellationToken))
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = exp
            };
            var json = _json.ToJson(value);
            await _distributedCache.SetStringAsync(key.ToString(), json, options, token);
        }

        public async Task<(bool success, T value)> TryGetAsync<T>(object key, CancellationToken token = default(CancellationToken))
        {
            var json = await _distributedCache.GetStringAsync(key.ToString(), token);
            if (json == null) return (false, default(T));
            var value = _json.ToObject<T>(json);
            return (true, value);
        }
    }
}
