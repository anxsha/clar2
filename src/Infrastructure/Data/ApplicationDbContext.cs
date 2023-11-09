using System.Reflection;
using neatbook.Application.Common.Interfaces;
using neatbook.Domain.Entities;
using neatbook.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using neatbook.Domain;
using neatbook.Domain.Notes;

namespace neatbook.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<TodoList> TodoLists => Set<TodoList>();

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    
    public DbSet<Note> Notes => Set<Note>();
  
    public DbSet<NoteCollaborator> NoteCollaborators => Set<NoteCollaborator>();
  
    public DbSet<Label> Labels => Set<Label>();
  
    public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
