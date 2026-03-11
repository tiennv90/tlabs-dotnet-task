using System.Collections.Generic;
using System.Linq;
using TestApp.ToDoList.Entity;
using TestApp.ToDoList.Store;

namespace TestApp.ToDoList.Repository
{
    /// <summary>
    /// Repository for managing Tag entity
    /// </summary>
    public class TagRepository : ITagRepository
    {
        private readonly ToDoListDbContext context;

        public TagRepository(ToDoListDbContext context)
        {
            this.context = context;
        }

        /// <inheritdoc/>
        public ICollection<Tag> GetAll()
        {
            return context.Tags.ToList();
        }

        /// <inheritdoc/>
        public Tag GetTagByName(string tagName)
        {
            return context.Tags
                .FirstOrDefault(t => t.Name.Equals(tagName, System.StringComparison.OrdinalIgnoreCase));
        }

        /// <inheritdoc/>
        public Tag Add(Tag tag)
        {
            context.Tags.Add(tag);
            context.SaveChanges();
            return tag;
        }
    }
}