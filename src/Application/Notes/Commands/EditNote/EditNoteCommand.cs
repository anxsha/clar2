using neatbook.Application.Common.Interfaces;
using neatbook.Domain.Notes;
using neatbook.Domain.Notes.Events;

namespace neatbook.Application.Notes.Commands.EditNote; 

public record EditNoteCommand
  (int NoteId, string Title, string Content, string UserId) : IRequest { }

public class EditNoteCommandHandler : IRequestHandler<EditNoteCommand> {
  private readonly IApplicationDbContext _context;

  public EditNoteCommandHandler(IApplicationDbContext context) {
    _context = context;
  }

  public async Task Handle(EditNoteCommand request, CancellationToken cancellationToken) {
    var entity = await _context.Notes
      .Include(n => n.Collaborators)
      .FirstOrDefaultAsync(n => n.Id == request.NoteId, cancellationToken);

    Guard.Against.NotFound(request.NoteId, entity);
    
    if (!entity.UserCanEdit(request.UserId)) {
      return;
    }

    entity.Modify(request.Title, request.Content);
    entity.AddDomainEvent(new NoteModifiedEvent(entity));
    await _context.SaveChangesAsync(cancellationToken);
  }
}
