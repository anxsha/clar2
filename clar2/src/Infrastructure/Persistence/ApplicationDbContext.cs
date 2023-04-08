using System.Reflection;
using clar2.Application.Common.Interfaces;
using clar2.Domain.Notes;
using clar2.Domain.ToDoItems;
using clar2.Domain.TodoLists;
using clar2.Domain.Users;
using clar2.Infrastructure.Identity;
using clar2.Infrastructure.Persistence.Interceptors;
using Duende.IdentityServer.EntityFramework.Options;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace clar2.Infrastructure.Persistence;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext {
  private readonly IMediator _mediator;
  private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

  public ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    IOptions<OperationalStoreOptions> operationalStoreOptions,
    IMediator mediator,
    AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
    : base(options, operationalStoreOptions) {
    _mediator = mediator;
    _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
  }

  public DbSet<TodoList> TodoLists => Set<TodoList>();

  public DbSet<TodoItem> TodoItems => Set<TodoItem>();
  
  public DbSet<Note> Notes => Set<Note>();
  
  public DbSet<NoteCollaborator> NoteCollaborators => Set<NoteCollaborator>();
  
  public DbSet<Label> Labels => Set<Label>();
  
  public new DbSet<User> Users => Set<User>();

  protected override void OnModelCreating(ModelBuilder builder) {
    builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    base.OnModelCreating(builder);
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
    optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
    await _mediator.DispatchDomainEvents(this);

    return await base.SaveChangesAsync(cancellationToken);
  }
}
