using System;
using System.Collections.Generic;
using System.Linq;
using TestApp.ToDoList.Entity;
using TestApp.ToDoList.Module;
using TestApp.ToDoList.Repository;
using TestApp.ToDoList.Pageable;
using TestApp.ToDoList.Cache;

namespace TestApp.ToDoList.Tracker
{
  /// <summary>
  /// Implementation of the to-do list tracking.
  /// </summary>
  public class ToDoListTracker : IToDoListTracker
  {
    private readonly ICacheSupplier cacheSupplier;

    private readonly ICacheHelper cacheHelper;

    private readonly IToDoItemsRepository repository;
    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="repository"></param>
    public ToDoListTracker(IToDoItemsRepository repository, ICacheSupplier cacheSupplier, ICacheHelper cacheHelper)
    {
      this.repository = repository;
      this.cacheSupplier = cacheSupplier;
      this.cacheHelper = cacheHelper;
    }

    /// <inheritdoc/>
    public ToDoItem AddItem(string title)
    {
      // Implementation for adding a to-do item
      var newItem = new ToDoItem { Title = title, IsCompleted = false };
      newItem = repository.Create(newItem);
      cacheHelper.InvalidateToDoTaskListCache();
      return newItem;
    }
    /// <inheritdoc/>
    public ToDoItem RemoveItem(int id)
    {
      var item = repository.GetItemById(id);
      if (null == item)
        throw new ArgumentException($"Item with id {id} not found");

      repository.Delete(id);
      cacheHelper.InvalidateToDoTaskListCache();
      return item;
    }
    /// <inheritdoc/>
    public ToDoItem GetItem(int id)
    {
      // Implementation for getting a specific to-do item
      var item = repository.GetItemById(id);
      if (null == item)
        throw new ArgumentException($"Item with id {id} not found");

      return item;
    }
    /// <inheritdoc/>
    public CursorPagedResponse<ToDoItem> GetAllItems(ToDoItemQueryParameters query)
    {
      string cacheKey = $"tasks:{query.IsCompleted}:{query.TagName}:{query.PageSize}:{query.LastCursorId}:{query.SortBy}:{query.Ascending}";
      
      // Fetch data from cache
      var cache = cacheSupplier.Get<CursorPagedResponse<ToDoItem>>(cacheKey);
      if (cache != null) return cache;

      var items = repository.GetAllItems(query);
      var pageSize = query.PageSize ?? 5;
      bool hasMore = items.Count > query.PageSize;
      var result = items.Take(pageSize).ToList();

      var response = new CursorPagedResponse<ToDoItem>
      {
        Items = result,
        HasMore = hasMore,
        NextCursorId = result.LastOrDefault()?.Id
      };

      cacheSupplier.Set(cacheKey, response, TimeSpan.FromMinutes(5));
      cacheHelper.InvalidateToDoTaskListCache();
      return response;
    }
    /// <inheritdoc/>
    public ToDoItem EditItem(int id, ToDoItem updatedTask)
    {
      var item = repository.GetItemById(id);
      if (null == item)
        throw new ArgumentException($"Item with id {id} not found");

      item.Title = updatedTask.Title;
      item.IsCompleted = updatedTask.IsCompleted;
      item.CompletedAt = updatedTask.IsCompleted ? DateTime.UtcNow : null;
      repository.Update(item);
      cacheHelper.InvalidateToDoTaskListCache();
      return item;
    }
  }
}