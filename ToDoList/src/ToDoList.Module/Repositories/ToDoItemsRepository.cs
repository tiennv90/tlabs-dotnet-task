using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TestApp.ToDoList.Entity;
using TestApp.ToDoList.Store;
using TestApp.ToDoList.Pageable;
using System;

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
          new ToDoItem { Title = "Morning Exercise", IsCompleted = false, Tags = new List<Tag> {homeTag}},
          new ToDoItem { Title = "Apply Loan", IsCompleted = false, Tags = new List<Tag> {financeTag}},
          new ToDoItem { Title = "Lease A Car", IsCompleted = false, Tags = new List<Tag> {financeTag}},
          new ToDoItem { Title = "Pay Mortgage", IsCompleted = false, Tags = new List<Tag> {financeTag}},
          new ToDoItem { Title = "Disk Washing", IsCompleted = false, Tags = new List<Tag> {homeTag}},
          new ToDoItem { Title = "Empty waste baskets", IsCompleted = false, Tags = new List<Tag> {homeTag}},
          new ToDoItem { Title = "Music Listening", IsCompleted = false, Tags = new List<Tag> {homeTag}},
          new ToDoItem { Title = "Cooking", IsCompleted = false, Tags = new List<Tag> {homeTag}}
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
      
      //Build Filter and Sort
      var specs = new List<IQuerySpecification<ToDoItem>>
      {
        new CompletedFilter(query.IsCompleted),
        new TagFilter(query.TagName),
        new SortSpecification(query.SortBy, query.Ascending),
        new CursorPagination(query)
      };

      //Apply Filter and Sort and Paging
      foreach (var spec in specs)
      {
        taskQuery = spec.Apply(taskQuery);
      }

      Console.WriteLine($"[DB] GetAllItems query: IsCompleted={query.IsCompleted}, TagName={query.TagName}");
        
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