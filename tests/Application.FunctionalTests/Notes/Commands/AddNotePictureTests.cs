using Microsoft.Extensions.DependencyInjection;
using neatbook.Application.Notes.Commands.AddNotePicture;
using neatbook.Domain.Notes;
using neatbook.Domain.Notes.Enums;
using neatbook.Infrastructure.Data;

namespace neatbook.Application.FunctionalTests.Notes.Commands;

using static Testing;

public class AddNotePictureTests : BaseTestFixture {
  private readonly ApplicationDbContext _context;
  public AddNotePictureTests() {
    var scopeFactory = GetScopeFactory();
    _context = scopeFactory.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
  }
  [Test]
  public async Task OwnerCanAddThreePicturesToNote() {
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

    // Add three pictures to the note
    var cmd = new AddNotePictureCommand(entity.Id, "uploads/note-pictures/1.jpg", ownerId);
    await SendAsync(cmd);
    cmd = new AddNotePictureCommand(entity.Id, "uploads/note-pictures/2.jpg", ownerId);
    await SendAsync(cmd);
    cmd = new AddNotePictureCommand(entity.Id, "uploads/note-pictures/3.jpg", ownerId);
    await SendAsync(cmd);

    var note = await FindAsync<Note>(entity.Id);
    // load note pictures from the context
    await _context.Entry(note!).Collection(n => n.Pictures).LoadAsync();

    note.Should().NotBeNull();
    note!.Pictures.Should().HaveCount(3);
  }

  [Test]
  public async Task ReadWriteCollaboratorCanAddThreePicturesToNote() {
    var noteCollaborator = await RunAsUserAsync("note@collaborator", "Testing1234!", Array.Empty<string>());

    var ownerId = await RunAsDefaultUserAsync();

    var entity = new Note("Next week exams",
      """
      Monday – Math
      Tuesday – Physics
      Friday – Programming
      """,
      ownerId, NoteBackground.Red);
    entity.AddCollaborator(noteCollaborator, CollaboratorPermissions.ReadWrite);
    entity.AddLabel("school");
    entity.AddLabel("education");
    entity.AddLabel("exams");
    entity.AddLabel("collaborated");
    await AddAsync(entity);

    // Add three pictures to the note
    var cmd = new AddNotePictureCommand(entity.Id, "uploads/note-pictures/1.jpg", noteCollaborator);
    await SendAsync(cmd);
    cmd = new AddNotePictureCommand(entity.Id, "uploads/note-pictures/2.jpg", noteCollaborator);
    await SendAsync(cmd);
    cmd = new AddNotePictureCommand(entity.Id, "uploads/note-pictures/3.jpg", noteCollaborator);
    await SendAsync(cmd);

    var note = await FindAsync<Note>(entity.Id);
    // load note pictures from the context
    await _context.Entry(note!).Collection(n => n.Pictures).LoadAsync();
    
    note.Should().NotBeNull();
    note!.Pictures.Should().HaveCount(3);
  }

  [Test]
  public async Task ReadCollaboratorCannotAddPicturesToNote() {
    var noteCollaborator = await RunAsUserAsync("note@collaborator", "Testing1234!", Array.Empty<string>());

    var ownerId = await RunAsDefaultUserAsync();

    var entity = new Note("Next week exams",
      """
      Monday – Math
      Tuesday – Physics
      Friday – Programming
      """,
      ownerId, NoteBackground.Red);
    entity.AddCollaborator(noteCollaborator, CollaboratorPermissions.Read);
    entity.AddLabel("school");
    entity.AddLabel("education");
    entity.AddLabel("exams");
    entity.AddLabel("collaborated");
    await AddAsync(entity);

    var cmd = new AddNotePictureCommand(entity.Id, "uploads/note-pictures/1.jpg", noteCollaborator);
    await SendAsync(cmd);

    var note = await FindAsync<Note>(entity.Id);
    // load note pictures from the context
    await _context.Entry(note!).Collection(n => n.Pictures).LoadAsync();
    
    note.Should().NotBeNull();
    note!.Pictures.Should().HaveCount(0);
  }
  
  [Test]
  public async Task RandomUserCannotAddPicturesToNote() {
    var randomUser = await RunAsUserAsync("random@user", "Testing1234!", Array.Empty<string>());

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

    var cmd = new AddNotePictureCommand(entity.Id, "uploads/note-pictures/1.jpg", randomUser);
    await SendAsync(cmd);

    var note = await FindAsync<Note>(entity.Id);
    // load note pictures from the context
    await _context.Entry(note!).Collection(n => n.Pictures).LoadAsync();
    
    note.Should().NotBeNull();
    note!.Pictures.Should().HaveCount(0);
  }

  [Test]
  public async Task MaximumThreePicturesCanBeAddedToNote() {
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
    entity.AddPicture("/uploads/note-pictures/1.jpg");
    entity.AddPicture("/uploads/note-pictures/2.jpg");
    entity.AddPicture("/uploads/note-pictures/3.jpg");
    await AddAsync(entity);

    var cmd = new AddNotePictureCommand(entity.Id, "uploads/note-pictures/4.jpg", ownerId);
    await SendAsync(cmd);

    var note = await FindAsync<Note>(entity.Id);
    // load note pictures from the context
    await _context.Entry(note!).Collection(n => n.Pictures).LoadAsync();
    
    note.Should().NotBeNull();
    note!.Pictures.Should().HaveCount(3);
  }
}
