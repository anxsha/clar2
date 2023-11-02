using clar2.Application.Common.Interfaces;
using clar2.Domain.Notes;
using clar2.Domain.Notes.Enums;
using clar2.Domain.Notes.Events;
using MediatR;

namespace clar2.Application.Notes.Commands.CreateNote;

public record CreateNoteCommand
  (string Title, string Content, string UserId, NoteBackground Background = NoteBackground.Default) : IRequest<int> { }

public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, int> {
  private readonly IApplicationDbContext _context;

  public CreateNoteCommandHandler(IApplicationDbContext context) {
    _context = context;
  }

  public async Task<int> Handle(CreateNoteCommand request, CancellationToken cancellationToken) {
    var entity = new Note(request.Title, request.Content, request.UserId, request.Background);
    
    entity.AddDomainEvent(new NoteCreatedEvent(entity));
    
    _context.Notes.Add(entity);

    await _context.SaveChangesAsync(cancellationToken);

    return entity.Id;
  }
}
