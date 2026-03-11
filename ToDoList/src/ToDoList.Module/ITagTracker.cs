using TestApp.ToDoList.Entity;

namespace TestApp.ToDoList.Module
{
    public interface ITagTracker
    {
        Tag AddTag(int taskId, string tagName);
        Tag RemoveTag(int taskId, string tagName);
    }
}