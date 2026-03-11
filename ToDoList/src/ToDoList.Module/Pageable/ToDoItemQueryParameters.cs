using System;
using TestApp.ToDoList.Pageable;

namespace TestApp.ToDoList.Pageable
{
    public class ToDoItemQueryParameters : BasePageableQueryParamters
    {
        public bool? IsCompleted { get; set; }
        public string TagName { get; set; }
    }
}