using System;

namespace TestApp.ToDoList.Pageable
{
    public class BasePageableQueryParamters
    {
        public string SortBy { get; set; } = "CreatedAt";
        public bool Ascending { get; set; } = true;
        public int? PageSize { get; set; } = 20;
        public int? LastCursorId { get; set; } = null; 

    }
}