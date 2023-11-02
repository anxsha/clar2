using clar2.Application.Common.Exceptions;
using clar2.Application.Common.Interfaces;
using clar2.Domain.Notes;
using clar2.Domain.Notes.Events;
using MediatR;

namespace clar2.Application.Notes.Commands.EditNote;

public record EditNoteCommand
  (int NoteId, string Title, string Content, string UserId) : IRequest { }

public class EditNoteCommandHandler : IRequestHandler<EditNoteCommand> {
  private readonly IApplicationDbContext _context;

  public EditNoteCommandHandler(IApplicationDbContext context) {
    _context = context;
  }

  public async Task<Unit> Handle(EditNoteCommand request, CancellationToken cancellationToken) {
    var entity = await _context.Notes
      .FindAsync(new object[] {request.NoteId}, cancellationToken);

    if (entity is null) {
      throw new NotFoundException(nameof(Note), request.NoteId);
    }

    if (!entity.UserCanEdit(request.UserId)) {
      return Unit.Value;
    }

    entity.Modify(request.Title, request.Content);
    entity.AddDomainEvent(new NoteModifiedEvent(entity));
    await _context.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}
