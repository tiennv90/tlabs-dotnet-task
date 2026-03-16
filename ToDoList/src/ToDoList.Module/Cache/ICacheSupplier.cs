using System;

namespace TestApp.ToDoList.Cache
{
   public interface ICacheSupplier
   {
        T? Get<T>(string key);
        void Set<T>(string key, T value, TimeSpan? ttl = null);
        void Remove(string key);
   } 
}


