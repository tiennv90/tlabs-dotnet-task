namespace TestApp.ToDoList.Cache
{
    public interface ICacheHelper
    {
        public void InvalidateToDoTaskListCache();
        public void registerKey(string key);

    }
}