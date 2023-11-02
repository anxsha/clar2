using clar2.Application.Common.Exceptions;
using clar2.Application.Common.Interfaces;
using clar2.Domain.Notes;
using clar2.Domain.Notes.Events;
using MediatR;

namespace clar2.Application.Notes.Commands.ArchiveNote;

public record ArchiveNoteCommand
  (int NoteId, string UserId) : IRequest { }

public class ArchiveNoteCommandHandler : IRequestHandler<ArchiveNoteCommand> {
  private readonly IApplicationDbContext _context;

  public ArchiveNoteCommandHandler(IApplicationDbContext context) {
    _context = context;
  }

  public async Task<Unit> Handle(ArchiveNoteCommand request, CancellationToken cancellationToken) {
    var entity = await _context.Notes
      .FindAsync(new object[] {request.NoteId}, cancellationToken);

    if (entity is null) {
      throw new NotFoundException(nameof(Note), request.NoteId);
    }

    if (entity.OwnerId != request.UserId) {
      return Unit.Value;
    }

    entity.Archive();
    await _context.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}
