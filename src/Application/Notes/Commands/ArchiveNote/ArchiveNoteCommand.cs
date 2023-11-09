using neatbook.Application.Common.Interfaces;
using neatbook.Domain.Notes;

namespace neatbook.Application.Notes.Commands.ArchiveNote; 

public record ArchiveNoteCommand
  (int NoteId, string UserId) : IRequest { }

public class ArchiveNoteCommandHandler : IRequestHandler<ArchiveNoteCommand> {
  private readonly IApplicationDbContext _context;

  public ArchiveNoteCommandHandler(IApplicationDbContext context) {
    _context = context;
  }

  public async Task Handle(ArchiveNoteCommand request, CancellationToken cancellationToken) {
    var entity = await _context.Notes
      .FindAsync(new object[] {request.NoteId}, cancellationToken);
    
    Guard.Against.NotFound(request.NoteId, entity);
    
    if (entity.OwnerId != request.UserId) {
      return;
    }

    entity.Archive();
    await _context.SaveChangesAsync(cancellationToken);
  }
}
