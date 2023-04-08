using clar2.Domain.Notes.Enums;

namespace clar2.Domain.Notes; 

public class NoteCollaborator : BaseEntity {
  public required int CollaboratorId { get; set; }
  public required int NoteId { get; set; }
  public required CollaboratorPermissions Permissions { get; set; }
  
}
