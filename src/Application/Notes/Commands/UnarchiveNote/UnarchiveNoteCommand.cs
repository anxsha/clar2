using neatbook.Application.Common.Interfaces;

namespace neatbook.Application.Notes.Commands.UnarchiveNote;

public record UnarchiveNoteCommand(int NoteId, string UserId) : IRequest { }

public class UnarchiveNoteCommandHandler : IRequestHandler<UnarchiveNoteCommand> {
  private readonly IApplicationDbContext _context;

  public UnarchiveNoteCommandHandler(IApplicationDbContext context) {
    _context = context;
  }

  public async Task Handle(UnarchiveNoteCommand request, CancellationToken cancellationToken) {
    var entity = await _context.Notes
      .FindAsync(new object[] {request.NoteId}, cancellationToken);

    Guard.Against.NotFound(request.NoteId, entity);

    if (entity.OwnerId != request.UserId) {
      throw new NotFoundException(request.NoteId.ToString(), nameof(entity));
    }

    entity.Unarchive();
    await _context.SaveChangesAsync(cancellationToken);
  }
}
