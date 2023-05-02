using clar2.Domain;
using clar2.Domain.Notes;
using clar2.Domain.ToDoItems;
using clar2.Domain.TodoLists;
using Microsoft.EntityFrameworkCore;

namespace clar2.Application.Common.Interfaces;

public interface IApplicationDbContext {
  DbSet<TodoList> TodoLists { get; }

  DbSet<TodoItem> TodoItems { get; }
  
  DbSet<Note> Notes { get; }
  
  DbSet<ApplicationUser> ApplicationUsers { get; }

  Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
