using System;
using System.Runtime.InteropServices.Marshalling;
using Microsoft.Extensions.Caching.Memory;

namespace TestApp.ToDoList.Cache
{
    public class MemoryCacheSupplier : ICacheSupplier
    {
        private readonly IMemoryCache cache;

        public MemoryCacheSupplier(IMemoryCache cache)
        {
            this.cache = cache;
        }

        public T? Get<T>(string key)
        {
            cache.TryGetValue(key, out T value);
            Console.WriteLine($"[CACHE] GetAllItems key: {key}");
            return value;
        }

        public void Set<T>(string key, T value, TimeSpan? ttl = null)
        {
            if (ttl.HasValue)
            {
                cache.Set(key, value, ttl.Value);
            } else
            {
                cache.Set(key, value);
            }
        }

        public void Remove(string key)
        {
            cache.Remove(key);
        }
    }
}