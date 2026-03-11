using System.Collections.Generic;
using TestApp.ToDoList.Entity;

namespace TestApp.ToDoList.Repository
{
    public interface ITagRepository
    {
        /// <summary>
        /// Repository interface for managing tags.
        /// </summary>
        ICollection<Tag> GetAll();

        /// <summary>
        /// fetch Tag By name
        /// </summary>
        Tag GetTagByName(string tagName);

        /// <summary>
        /// Add a new Tag
        /// </summary>
        Tag Add(Tag tag);
    }
}