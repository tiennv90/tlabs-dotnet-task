
using System.Linq;
using TestApp.ToDoList.Entity;

namespace TestApp.ToDoList.Pageable
{
    public class CompletedFilter : IQuerySpecification<ToDoItem>
    {
        private readonly bool? isCompleted;

        public CompletedFilter(bool? isCompleted)
        {
            this.isCompleted = isCompleted;
        }

        public IQueryable<ToDoItem> Apply(IQueryable<ToDoItem> query)
        {
            if (!isCompleted.HasValue)
                return query;

            return query.Where(t => t.IsCompleted == isCompleted.Value);
        }
    }
}