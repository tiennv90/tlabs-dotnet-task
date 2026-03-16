using System.Collections.Generic;

namespace TestApp.ToDoList.Cache
{
    // Store all keys for TaskDoDoItem
    public static class ToDoItemsCacheKeys
    {
        public static HashSet<string> AllKeys { get; } = new HashSet<string>();
    }
}