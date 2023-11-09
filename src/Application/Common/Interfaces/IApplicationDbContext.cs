using neatbook.Domain;
using neatbook.Domain.Entities;
using neatbook.Domain.Notes;

namespace neatbook.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }
    
    DbSet<Note> Notes { get; }
  
    DbSet<ApplicationUser> ApplicationUsers { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
