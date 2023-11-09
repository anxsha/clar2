using neatbook.Domain.Notes.Enums;

namespace neatbook.Domain.Notes; 

public class NoteCollaborator : BaseEntity {
  public string CollaboratorId { get; private set; }
  public int NoteId { get; private set; }
  public CollaboratorPermissions Permissions { get; private set; }

  public NoteCollaborator(string collaboratorId, int noteId, CollaboratorPermissions permissions = CollaboratorPermissions.Read) {
    CollaboratorId = collaboratorId;
    NoteId = noteId;
    Permissions = permissions;
  }

  public void ChangeCollaboratorPermissions(CollaboratorPermissions permissions) {
    Permissions = permissions;
  }
}

