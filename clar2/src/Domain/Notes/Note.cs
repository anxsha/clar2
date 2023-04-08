using clar2.Domain.Common.Interfaces;
using clar2.Domain.Notes.Enums;
using clar2.Domain.Users;

namespace clar2.Domain.Notes;

public class Note : BaseAuditableEntity, IAggregateRoot {
  public string? Title { get; set; }
  public required string Content { get; set; }
  public required NoteBackground Background { get; set; }
  public List<NotePicture> Pictures { get; set; } = new();
  public User? Owner { get; set; }
  public List<NoteCollaborator> Collaborators { get; set; } = new();
  public required bool IsArchived { get; set; }

  // public void AddCollaborator(User collaborator, CollaboratorPermissions permissions = CollaboratorPermissions.Read) {
  //   if (collaborator.Equals(Owner)) {
  //     throw new ArgumentException("Cannot add the owner as a collaborator.", nameof(collaborator));
  //   }
  //   // If that user is already a collaborator, just change their permissions to the new ones
  //   NoteCollaborator? noteCollaborator = Collaborators.FirstOrDefault(c => c.CollaboratorId == collaborator.Id);
  //   if (noteCollaborator is not null) {
  //     noteCollaborator.Permissions = permissions;
  //   } else {
  //     Collaborators.Add(new NoteCollaborator() {
  //       CollaboratorId = collaborator.Id,
  //       NoteId = this.Id,
  //       Permissions = permissions
  //     });
  //   }
  // }
  //
  // public void RemoveCollaborator(User collaborator) {
  //   NoteCollaborator? noteCollaborator = Collaborators.FirstOrDefault(c => c.CollaboratorId == collaborator.Id);
  //   if (noteCollaborator is not null) {
  //     Collaborators.Remove(noteCollaborator);
  //   }
  // }
  //
  // public void AddPicture() {
  //   //todo
  //   Pictures.Add(new Picture("testurl"));
  // }
  //
  // public void RemovePicture() {
  //   //todo
  // }
}
