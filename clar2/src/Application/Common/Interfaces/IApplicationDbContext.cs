using clar2.Domain.ToDoItems;
using clar2.Domain.TodoLists;
using Microsoft.EntityFrameworkCore;

namespace clar2.Application.Common.Interfaces;

public interface IApplicationDbContext {
  DbSet<TodoList> TodoLists { get; }

  DbSet<TodoItem> TodoItems { get; }

  Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
