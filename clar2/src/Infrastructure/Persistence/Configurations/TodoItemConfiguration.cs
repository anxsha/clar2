using clar2.Domain.ToDoItems;
using clar2.Domain.TodoLists;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace clar2.Infrastructure.Persistence.Configurations;

public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem> {
  public void Configure(EntityTypeBuilder<TodoItem> builder) {
    builder.Property(t => t.Title)
      .HasMaxLength(200)
      .IsRequired();
  }
}
