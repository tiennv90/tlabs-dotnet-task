using Microsoft.EntityFrameworkCore;

using TestApp.ToDoList.Entity;

namespace TestApp.ToDoList.Store
{
  /// <summary>ToDoList's EF model.</summary>
  public class ToDoListEntityModel
  {
    /// <inheritdoc/>
    public void ConfigureModel(ModelBuilder modBuilder)
    {

      modBuilder.Entity<ToDoItem>(s =>
      {
        s.HasKey(d => d.Id);
        s.HasIndex(d => d.Title)
            .IsUnique();

        s.HasMany(t => t.Tags)
            .WithMany()
            .UsingEntity(j => j.ToTable("ToDoItemTag"));            
      });

      modBuilder.Entity<Tag>(tag =>
      {
          tag.HasKey(t => t.Id);
          tag.HasIndex(t => t.Name).IsUnique();
      });
    }
  }
}