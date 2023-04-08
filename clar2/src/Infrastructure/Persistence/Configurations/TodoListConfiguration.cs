using clar2.Domain.TodoLists;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace clar2.Infrastructure.Persistence.Configurations;

public class TodoListConfiguration : IEntityTypeConfiguration<TodoList> {
  public void Configure(EntityTypeBuilder<TodoList> builder) {
    builder.Property(t => t.Title)
      .HasMaxLength(200)
      .IsRequired();

    builder
      .OwnsOne(b => b.Colour);
  }
}