using neatbook.Application.Common.Interfaces;
using neatbook.Domain.Notes.Events;

namespace neatbook.Application.Notes.Commands.DeleteNote; 

public record DeleteNoteCommand
  (int NoteId, string UserId) : IRequest { }

public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand> {
  private readonly IApplicationDbContext _context;

  public DeleteNoteCommandHandler(IApplicationDbContext context) {
    _context = context;
  }

  public async Task Handle(DeleteNoteCommand request, CancellationToken cancellationToken) {
    var entity = await _context.Notes
      .FindAsync(new object[] {request.NoteId}, cancellationToken);

    Guard.Against.NotFound(request.NoteId, entity);

    if (entity.OwnerId != request.UserId) {
      throw new NotFoundException(request.NoteId.ToString(), nameof(entity));
    }

    _context.Notes.Remove(entity);
    entity.AddDomainEvent(new NoteDeletedEvent(entity));
    await _context.SaveChangesAsync(cancellationToken);
  }
}
