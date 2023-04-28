using clar2.Domain.Notes.Enums;
using clar2.Domain.Users;

namespace clar2.Domain.Notes; 

public class NoteCollaborator : BaseEntity {
  public int CollaboratorId { get; private set; }
  public int NoteId { get; private set; }
  public CollaboratorPermissions Permissions { get; private set; }

  public NoteCollaborator(User collaborator, Note note, CollaboratorPermissions permissions = CollaboratorPermissions.Read) {
    CollaboratorId = collaborator.Id;
    NoteId = note.Id;
    Permissions = permissions;
  }

  public void ChangeCollaboratorPermissions(CollaboratorPermissions permissions) {
    Permissions = permissions;
  }
}
