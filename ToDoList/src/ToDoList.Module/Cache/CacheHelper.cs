using System;

namespace TestApp.ToDoList.Cache
{
    public class CacheHelper : ICacheHelper
    {
        private readonly ICacheSupplier cacheSupplier;

        public CacheHelper(ICacheSupplier cacheSupplier)
        {
            this.cacheSupplier = cacheSupplier;
        }

        public void InvalidateToDoTaskListCache()
        {
            // Assumption: Simple Memory cache stores all currently used keys
            var keys = ToDoItemsCacheKeys.AllKeys;
            foreach (var key in keys)
            {
                cacheSupplier.Remove(key);
            }

            // reset list keys
            ToDoItemsCacheKeys.AllKeys.Clear();
        }

        public void registerKey(string key)
        {
            ToDoItemsCacheKeys.AllKeys.Add(key);
        }

    }
}