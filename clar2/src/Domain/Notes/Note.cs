using clar2.Domain.Common.Interfaces;
using clar2.Domain.Notes.Enums;
using clar2.Domain.Users;

namespace clar2.Domain.Notes;

public class Note : BaseAuditableEntity, IAggregateRoot {
  public string Title { get; private set; }
  public string Content { get; private set; }
  public NoteBackground Background { get; private set; }
  public List<NotePicture> Pictures { get; private set; } = new();
  public int OwnerId { get; private set; }
  public List<NoteCollaborator> Collaborators { get; private set; } = new();
  public List<Label> Labels { get; private set; } = new();
  public bool IsArchived { get; private set; }

  public Note(string title, string content, User owner, NoteBackground background = NoteBackground.Default) {
    Title = title;
    Content = content;
    Background = background;
    OwnerId = owner.Id;
    IsArchived = false;
  }

  public void AddCollaborator(User collaborator, CollaboratorPermissions permissions = CollaboratorPermissions.Read) {
    if (collaborator.Id == OwnerId) {
      throw new ArgumentException("Cannot add the owner as a collaborator.", nameof(collaborator));
    }
    // If that user is already a collaborator, just change their permissions to the new ones
    NoteCollaborator? noteCollaborator = Collaborators.FirstOrDefault(c => c.CollaboratorId == collaborator.Id);
    if (noteCollaborator is not null) {
      noteCollaborator.ChangeCollaboratorPermissions(permissions);
    } else {
      Collaborators.Add(new NoteCollaborator( 
        collaborator,
        this,
        permissions
      ));
    }
  }
  
  public void RemoveCollaborator(User collaborator) {
    NoteCollaborator? noteCollaborator = Collaborators.FirstOrDefault(c => c.CollaboratorId == collaborator.Id);
    if (noteCollaborator is not null) {
      Collaborators.Remove(noteCollaborator);
    }
  }
  
  public void AddPicture() {
    //todo
    // Pictures.Add(new NotePicture(""));
  }
  
  public void RemovePicture() {
    //todo
  }

  public void AddLabel() {
    //todo
  }

  public void RemoveLabel() {
    //todo
  }
}
