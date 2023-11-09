using neatbook.Application.Notes.Commands.DeleteNote;
using neatbook.Domain.Notes;
using neatbook.Domain.Notes.Enums;

namespace neatbook.Application.FunctionalTests.Notes.Commands; 

using static Testing;

public class DeleteNoteTests : BaseTestFixture {
  [Test]
  public async Task OwnerCanDeleteNote() {
    // Current user, note owner
    var ownerId = await RunAsDefaultUserAsync();

    var entity = new Note("Next week exams",
      """
      Monday – Math
      Tuesday – Physics
      Friday – Programming
      """,
      ownerId, NoteBackground.Red);
    entity.AddLabel("school");
    entity.AddLabel("education");
    entity.AddLabel("exams");
    await AddAsync(entity);

    // Removing a note should be possible only by the owner
    var cmd = new DeleteNoteCommand(entity.Id, ownerId);
    await SendAsync(cmd);
    var note = await FindAsync<Note>(entity.Id);
    // Note should be removed
    note.Should().BeNull();
  }

  [Test]
  public async Task NoteCollaboratorCannotDeleteNote() {
    var noteCollaboratorId = await RunAsUserAsync("note@collaborator", "Testing1234!", Array.Empty<string>());

    // Current user, note owner
    var ownerId = await RunAsDefaultUserAsync();

    var entity = new Note("Next week exams",
      """
      Monday – Math
      Tuesday – Physics
      Friday – Programming
      """,
      ownerId, NoteBackground.Red);
    entity.AddCollaborator(noteCollaboratorId, CollaboratorPermissions.ReadWrite);
    entity.AddLabel("school");
    entity.AddLabel("education");
    entity.AddLabel("exams");
    entity.AddLabel("collaborated");
    await AddAsync(entity);

    // Removing a note by a note collaborator should not work
    var cmd = new DeleteNoteCommand(entity.Id, noteCollaboratorId);
    await SendAsync(cmd);
    var note = await FindAsync<Note>(entity.Id);
    note.Should().NotBeNull();
  }

  [Test]
  public async Task RandomUserCannotDeleteNote() {
    var randomUserId = await RunAsUserAsync("random@user", "Testing1234!", Array.Empty<string>());

    // Current user, note owner
    var ownerId = await RunAsDefaultUserAsync();

    var entity = new Note("Next week exams",
      """
      Monday – Math
      Tuesday – Physics
      Friday – Programming
      """,
      ownerId, NoteBackground.Red);
    entity.AddLabel("school");
    entity.AddLabel("education");
    entity.AddLabel("exams");
    entity.AddLabel("collaborated");
    await AddAsync(entity);

    // Removing a note by a random user should not work
    var cmd = new DeleteNoteCommand(entity.Id, randomUserId);
    await SendAsync(cmd);
    var note = await FindAsync<Note>(entity.Id);
    note.Should().NotBeNull();
  }
}
