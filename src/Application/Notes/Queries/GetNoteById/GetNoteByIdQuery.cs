using neatbook.Application.Common.Interfaces;

namespace neatbook.Application.Notes.Queries.GetNoteById;

public sealed record GetNoteByIdQuery(int NoteId, string UserId) : IRequest<NoteDto>;

public class
  GetNoteByIdQueryHandler : IRequestHandler<GetNoteByIdQuery, NoteDto> {
  private readonly IApplicationDbContext _context;

  public GetNoteByIdQueryHandler(IApplicationDbContext context) {
    _context = context;
  }

  public async Task<NoteDto> Handle(GetNoteByIdQuery request,
    CancellationToken cancellationToken) {
    var entity = await _context.Notes
      .Include(n => n.Pictures)
      .Include(n => n.Collaborators)
      .Where(n => n.Id == request.NoteId)
      .Select(n => NoteDto.MapFromNote(n))
      .FirstOrDefaultAsync(cancellationToken);

    Guard.Against.NotFound(request.NoteId, entity);

    if (!entity.UserCanView(request.UserId)) {
      throw new NotFoundException(request.NoteId.ToString(), nameof(entity));
    }

    return entity;
  }
}
