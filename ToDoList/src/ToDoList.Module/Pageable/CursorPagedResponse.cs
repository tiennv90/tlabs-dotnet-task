using System.Collections;
using System.Collections.Generic;

namespace TestApp.ToDoList.Pageable
{
    public class CursorPagedResponse<T>
    {
        public ICollection<T> Items { get; set; }
        
        public int? NextCursorId {get; set;}

        public bool HasMore {get; set;}
    }
}