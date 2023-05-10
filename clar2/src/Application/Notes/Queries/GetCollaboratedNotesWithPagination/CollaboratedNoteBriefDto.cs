using AutoMapper;
using clar2.Domain.Notes;
using clar2.Domain.Notes.Enums;

namespace clar2.Application.Notes.Queries.GetCollaboratedNotesWithPagination; 

public class CollaboratedNoteBriefDto : AuthoredNoteBriefDto {
  public bool IsArchived { get; set; }

  public CollaboratorPermissions Permissions => _collaborators
    .Where(c => c.CollaboratorId == CurrentUserId)
    .Select(p => p.Permissions).FirstOrDefault();
  public string CurrentUserId { get; set; }
  private List<NoteCollaborator> _collaborators = new();
  
  public override void Mapping(Profile profile) {
    profile.CreateMap<Note, CollaboratedNoteBriefDto>()
      .IncludeBase<Note, AuthoredNoteBriefDto>()
      .ForMember(d => d._collaborators,
        opt => opt.MapFrom(
          s => s.Collaborators));
  }
}
