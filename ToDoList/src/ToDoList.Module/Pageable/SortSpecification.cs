using System.Linq;
using TestApp.ToDoList.Entity;

namespace TestApp.ToDoList.Pageable
{    
    public class SortSpecification : IQuerySpecification<ToDoItem>
    {
        private readonly string sortBy;
        private readonly bool ascending;

        public SortSpecification(string sortBy, bool ascending)
        {
            this.sortBy = sortBy;
            this.ascending = ascending;
        }

        public IQueryable<ToDoItem> Apply(IQueryable<ToDoItem> query)
        {
            return sortBy switch
            {
                "Title" => ascending ? query.OrderBy(t => t.Title)
                                    : query.OrderByDescending(t => t.Title),

                "CompletedAt" => ascending ? query.OrderBy(t => t.CompletedAt)
                                        : query.OrderByDescending(t => t.CompletedAt),                        

                _ => ascending ? query.OrderBy(t => t.CreatedAt)
                            : query.OrderByDescending(t => t.CreatedAt)
            };
        }
    }    
}