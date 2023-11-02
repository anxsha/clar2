using clar2.Application.Common.Exceptions;
using clar2.Application.Common.Interfaces;
using clar2.Domain.Notes;
using clar2.Domain.Notes.Events;
using MediatR;

namespace clar2.Application.Notes.Commands.DeleteNote;

public record DeleteNoteCommand
  (int NoteId, string UserId) : IRequest { }

public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand> {
  private readonly IApplicationDbContext _context;

  public DeleteNoteCommandHandler(IApplicationDbContext context) {
    _context = context;
  }

  public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken) {
    var entity = await _context.Notes
      .FindAsync(new object[] {request.NoteId}, cancellationToken);

    if (entity is null) {
      throw new NotFoundException(nameof(Note), request.NoteId);
    }

    if (entity.OwnerId != request.UserId) {
      return Unit.Value;
    }

    _context.Notes.Remove(entity);
    entity.AddDomainEvent(new NoteDeletedEvent(entity));
    await _context.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}
