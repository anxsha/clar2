using neatbook.Domain;
using neatbook.Domain.Notes;

namespace neatbook.Application.Common.Interfaces;

public interface IApplicationDbContext {
  DbSet<Note> Notes { get; }
  DbSet<ApplicationUser> ApplicationUsers { get; }
  Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
