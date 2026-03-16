using TestApp.ToDoList.Entity;
using TestApp.ToDoList.Repository;
using System;
using System.Linq;

namespace TestApp.ToDoList.Module
{
    public class TagTracker : ITagTracker
    {
        private readonly IToDoItemsRepository taskRepository;
        private readonly ITagRepository tagRepository;

        public TagTracker(IToDoItemsRepository taskRepo, ITagRepository tagRepo)
        {
            taskRepository = taskRepo;
            tagRepository = tagRepo;
        }

        public Tag AddTag(int taskId, string tagName)
        {
            var item = taskRepository.GetItemById(taskId);
            if (item == null) throw new ArgumentException($"Task {taskId} not found");

            //verify tag In Task
            var existingTagInTask = item.Tags
                .FirstOrDefault(t => t.Name.Equals(tagName, StringComparison.OrdinalIgnoreCase));
            if (existingTagInTask != null) return existingTagInTask;

            //add Tag to Tag Entity
            var globalTag = tagRepository.GetTagByName(tagName);
            if (globalTag == null)
            {
                globalTag = new Tag { Name = tagName };
                tagRepository.Add(globalTag);
            }

            //add tag to Task and save Task
            item.Tags.Add(globalTag);
            taskRepository.Update(item); 

            return globalTag;
        }

        public Tag RemoveTag(int taskId, string tagName)
        {

            var item = taskRepository.GetItemById(taskId);
            if (item == null)
                throw new ArgumentException($"Task {taskId} not found");

            var tag = item.Tags
                .FirstOrDefault(t => t.Name.Equals(tagName, StringComparison.OrdinalIgnoreCase));

            if (tag != null)
            {
                item.Tags.Remove(tag);
                taskRepository.Update(item);
                return tag;
            }

            return null;
        }
    }
}