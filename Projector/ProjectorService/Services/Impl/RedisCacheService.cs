using Microsoft.Extensions.Caching.Distributed;
using ProjectorService.Services.Interfaces;
using System.Text.Json;

namespace ProjectorService.Services.Impl
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        public RedisCacheService(IDistributedCache distributedCache)
        {
            this._distributedCache = distributedCache;
        }

        public async Task Clear()
        {
            await this._distributedCache.RemoveAsync("test");
        }

        public async Task<T> Get<T>(string key)
        {
            var cacheItemValue = await this._distributedCache.GetStringAsync(key);
            if (cacheItemValue == null)
                return default;

            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(cacheItemValue, typeof(T));
            }

            return JsonSerializer.Deserialize<T>(cacheItemValue);
        }

        public async Task Set<T>(string key, T obj, DateTime? expireDate)
        {
            var distributedCacheEntryOption = new DistributedCacheEntryOptions();
            if (expireDate.HasValue)
            {
                distributedCacheEntryOption.AbsoluteExpiration = expireDate.Value;
            }

            string value = String.Empty;
            if (typeof(T) != typeof(string) && !typeof(T).IsValueType)
                value = JsonSerializer.Serialize(obj);
            else
                value = Convert.ToString(obj);

            await _distributedCache.SetStringAsync(key, JsonSerializer.Serialize(obj), distributedCacheEntryOption);
        }
    }
}
