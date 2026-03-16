
using System.Linq;
using TestApp.ToDoList.Entity;

namespace TestApp.ToDoList.Pageable
{
    public class TagFilter : IQuerySpecification<ToDoItem>
    {
        private readonly string tagName;

        public TagFilter(string tagName)
        {
            this.tagName = tagName;
        }

        public IQueryable<ToDoItem> Apply(IQueryable<ToDoItem> query)
        {
            if (string.IsNullOrEmpty(tagName))
                return query;
            return query.Where(t => t.Tags.Any(tag => tag.Name == tagName));
        }
    }
}