using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TestApp.ToDoList.Entity;
using TestApp.ToDoList.Store;
using TestApp.ToDoList.Pageable;

namespace TestApp.ToDoList.Repository
{
  /// <summary>
  /// Repository for managing to-do items.
  /// </summary>
  public class ToDoItemsRepository : IToDoItemsRepository
  {
    private readonly ToDoListDbContext context;
    /// <summary>
    /// Ctor. Seeds initial data.
    /// </summary>
    /// <param name="context"></param>
    public ToDoItemsRepository(ToDoListDbContext context)
    {
      this.context = context;

      if (!context.ToDoItems.Any())
      {
        var financeTag = new Tag { Name = "Finance" };
        var homeTag = new Tag { Name = "Home" };   
        context.Tags.AddRange(financeTag, homeTag); 
        context.ToDoItems.AddRange(
        new[] {
          new ToDoItem { Title = "Laundry", Tags = new List<Tag> {homeTag}},
          new ToDoItem { Title = "Grocery Shopping", IsCompleted = true, Tags = new List<Tag> {homeTag}},
          new ToDoItem { Title = "Pay Bills", Tags = new List<Tag> {financeTag}},
          new ToDoItem { Title = "Clean the House", IsCompleted = true, Tags = new List<Tag> {homeTag}},
        }
      );
        context.SaveChanges();
      }
    }
    /// <inheritdoc/>
    public ICollection<ToDoItem> GetAllItems(ToDoItemQueryParameters query)
    {
      //Allow chaining WHERE and ORDERBY condition
      var taskQuery = context.ToDoItems.AsQueryable();
          
      //Filter by completed
      if (query.IsCompleted.HasValue)
          taskQuery = taskQuery.Where(t => t.IsCompleted == query.IsCompleted.Value);

      //Filter by tag
      if (!string.IsNullOrEmpty(query.TagName))
          taskQuery = taskQuery.Where(t => t.Tags.Any(tag => tag.Name == query.TagName));

      //Sort
      taskQuery = query.SortBy switch
      {
          "Title" => query.Ascending ? taskQuery.OrderBy(t => t.Title) : taskQuery.OrderByDescending(t => t.Title),
          "CreatedAt" => query.Ascending ? taskQuery.OrderBy(t => t.CreatedAt) : taskQuery.OrderByDescending(t => t.CreatedAt),
          "CompletedAt" => query.Ascending ? taskQuery.OrderBy(t => t.CompletedAt) : taskQuery.OrderByDescending(t => t.CompletedAt),
          _ => taskQuery.OrderBy(t => t.Id)
      };
   
      return taskQuery.ToList();
    }

    /// <inheritdoc/>
    public ToDoItem GetItemById(int id)
    {
      return context.ToDoItems.Find(id);
    }
    /// <inheritdoc/>
    public ToDoItem Create(ToDoItem item)
    {
      context.ToDoItems.Add(item);
      context.SaveChanges();
      return item;
    }
    /// <inheritdoc/>
    public ToDoItem Update(ToDoItem item)
    {
      context.ToDoItems.Update(item);
      context.SaveChanges();
      return item;
    }
    /// <inheritdoc/>
    public void Delete(int id)
    {
      var item = context.ToDoItems.Find(id);
      if (item != null)
      {
        context.ToDoItems.Remove(item);
        context.SaveChanges();
      }
    }

  }
}