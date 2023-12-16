using neatbook.Application.Common.Interfaces;

namespace neatbook.Application.Notes.Commands.AddNotePicture;

public record AddNotePictureCommand
  (int NoteId, string pictureUrl, string UserId) : IRequest<bool> { }

public class AddNotePictureCommandHandler : IRequestHandler<AddNotePictureCommand, bool> {
  private readonly IApplicationDbContext _context;

  public AddNotePictureCommandHandler(IApplicationDbContext context) {
    _context = context;
  }

  public async Task<bool> Handle(AddNotePictureCommand request, CancellationToken cancellationToken) {
    var entity = await _context.Notes
      .Include(n => n.Collaborators)
      .Include(n =>n.Pictures)
      .FirstOrDefaultAsync(n => n.Id == request.NoteId, cancellationToken);

    Guard.Against.NotFound(request.NoteId, entity);
    
    if (!entity.UserCanEdit(request.UserId)) {
      throw new NotFoundException(request.NoteId.ToString(), nameof(entity));
    }

    var pictureAdded = entity.AddPicture(request.pictureUrl);
    
    if (pictureAdded) {
      await _context.SaveChangesAsync(cancellationToken);
      return true;
    }

    return false;
  }
}
