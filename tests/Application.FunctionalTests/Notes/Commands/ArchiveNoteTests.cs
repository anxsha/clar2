using neatbook.Application.Notes.Commands.ArchiveNote;
using neatbook.Domain.Notes;
using neatbook.Domain.Notes.Enums;

namespace neatbook.Application.FunctionalTests.Notes.Commands; 

using static Testing;

public class ArchiveNoteTests : BaseTestFixture {
  [Test]
  public async Task OwnerCanArchiveNote() {
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

    // Archiving a note should be possible only by the owner
    var cmd = new ArchiveNoteCommand(entity.Id, ownerId);
    await SendAsync(cmd);
    var note = await FindAsync<Note>(entity.Id);
    // Note should be archived
    note.Should().NotBeNull();
    note!.IsArchived.Should().BeTrue();
  }

  [Test]
  public async Task NoteCollaboratorCannotArchiveNote() {
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

    // Archiving a note by a note collaborator should not work
    var cmd = new ArchiveNoteCommand(entity.Id, noteCollaboratorId);
    await SendAsync(cmd);
    var note = await FindAsync<Note>(entity.Id);
    note.Should().NotBeNull();
    note!.IsArchived.Should().BeFalse();
  }

  [Test]
  public async Task RandomUserCannotArchiveNote() {
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
    await AddAsync(entity);

    // Archiving a note by a random user
    var cmd = new ArchiveNoteCommand(entity.Id, randomUserId);
    await SendAsync(cmd);
    var note = await FindAsync<Note>(entity.Id);
    note.Should().NotBeNull();
    note!.IsArchived.Should().BeFalse();
  }

  [Test]
  public async Task ArchivingNoteIsIdempotent() {
    // Current user, note owner
    var ownerId = await RunAsDefaultUserAsync();

    // Create an already archived note
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

    entity.Archive();

    await AddAsync(entity);

    var note = await FindAsync<Note>(entity.Id);
    note.Should().NotBeNull();
    note!.IsArchived.Should().BeTrue();

    // Archiving an archived note should not change its state
    var cmd = new ArchiveNoteCommand(entity.Id, ownerId);
    await SendAsync(cmd);
    note = await FindAsync<Note>(entity.Id);
    note.Should().NotBeNull();
    note!.IsArchived.Should().BeTrue();
  }
}

