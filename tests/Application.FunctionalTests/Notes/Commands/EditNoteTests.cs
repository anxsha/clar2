using neatbook.Application.Notes.Commands.EditNote;
using neatbook.Domain.Notes;
using neatbook.Domain.Notes.Enums;

namespace neatbook.Application.FunctionalTests.Notes.Commands; 

using static Testing;

public class EditNoteTests : BaseTestFixture {
  [Test]
  public async Task OwnerCanEditNote() {
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

    const string newContent = "modified content";
    const string newTitle = "modified title";

    var cmd = new EditNoteCommand(entity.Id, newTitle, newContent, ownerId);
    await SendAsync(cmd);

    var note = await FindAsync<Note>(entity.Id);
    note.Should().NotBeNull();
    note!.Title.Should().Be(newTitle);
    note.Content.Should().Be(newContent);
  }

  [Test]
  public async Task ReadWriteCollaboratorCanEditNote() {
    var noteCollaborator = await RunAsUserAsync("note@collaborator", "Testing1234!", Array.Empty<string>());

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
    entity.AddCollaborator(noteCollaborator, CollaboratorPermissions.ReadWrite);
    await AddAsync(entity);

    const string newContent = "modified content";
    const string newTitle = "modified title";

    var cmd = new EditNoteCommand(entity.Id, newTitle, newContent, noteCollaborator);
    await SendAsync(cmd);

    var note = await FindAsync<Note>(entity.Id);
    note.Should().NotBeNull();
    note!.Title.Should().Be(newTitle);
    note.Content.Should().Be(newContent);
  }

  [Test]
  public async Task ReadCollaboratorCannotEditNote() {
    var noteCollaborator = await RunAsUserAsync("note@collaborator", "Testing1234!", Array.Empty<string>());

    // Current user, note owner
    var ownerId = await RunAsDefaultUserAsync();

    const string originalTitle = "Next week exams";
    const string originalContent = """
                                   Monday – Math
                                   Tuesday – Physics
                                   Friday – Programming
                                   """;
    var entity = new Note(originalTitle, originalContent, ownerId, NoteBackground.Red);
    entity.AddLabel("school");
    entity.AddLabel("education");
    entity.AddLabel("exams");
    entity.AddLabel("collaborated");
    entity.AddCollaborator(noteCollaborator, CollaboratorPermissions.Read);
    await AddAsync(entity);

    const string newContent = "modified content";
    const string newTitle = "modified title";

    var cmd = new EditNoteCommand(entity.Id, newTitle, newContent, noteCollaborator);
    await SendAsync(cmd);

    var note = await FindAsync<Note>(entity.Id);
    note.Should().NotBeNull();
    note!.Title.Should().Be(originalTitle);
    note.Content.Should().Be(originalContent);
  }

  [Test]
  public async Task RandomUserCannotEditNote() {
    var randomUser = await RunAsUserAsync("random@user", "Testing1234!", Array.Empty<string>());

    // Current user, note owner
    var ownerId = await RunAsDefaultUserAsync();

    const string originalTitle = "Next week exams";
    const string originalContent = """
                                   Monday – Math
                                   Tuesday – Physics
                                   Friday – Programming
                                   """;
    var entity = new Note(originalTitle, originalContent, ownerId, NoteBackground.Red);
    entity.AddLabel("school");
    entity.AddLabel("education");
    entity.AddLabel("exams");
    await AddAsync(entity);

    const string newContent = "modified content";
    const string newTitle = "modified title";

    var cmd = new EditNoteCommand(entity.Id, newTitle, newContent, randomUser);
    await SendAsync(cmd);

    var note = await FindAsync<Note>(entity.Id);
    note.Should().NotBeNull();
    note!.Title.Should().Be(originalTitle);
    note.Content.Should().Be(originalContent);
  }
}
