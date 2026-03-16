using System.Linq;
using TestApp.ToDoList.Entity;

namespace TestApp.ToDoList.Pageable
{
    public class CursorPagination : IQuerySpecification<ToDoItem>{

        private readonly ToDoItemQueryParameters query;

        public CursorPagination(ToDoItemQueryParameters query)
        {
            this.query = query;
        }

        public IQueryable<ToDoItem> Apply(IQueryable<ToDoItem> taskQuery)
        {
            if (query.LastCursorId.HasValue)
                taskQuery = taskQuery.Where(t => t.Id > query.LastCursorId.Value);

            if (query.PageSize.HasValue)
                taskQuery = taskQuery.Take(query.PageSize.Value + 1);

            return taskQuery;
        }
    }
}