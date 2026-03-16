using System;
using System.Linq;

namespace TestApp.ToDoList.Pageable {

    public interface IQuerySpecification<T>
    {
        IQueryable<T> Apply(IQueryable<T> query);
    }
    
}