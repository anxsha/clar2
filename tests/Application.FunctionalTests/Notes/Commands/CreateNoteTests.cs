using neatbook.Application.Notes.Commands.CreateNote;
using neatbook.Domain.Notes;
using neatbook.Domain.Notes.Enums;

namespace neatbook.Application.FunctionalTests.Notes.Commands; 

using static Testing;

public class CreateNoteTests : BaseTestFixture {
  [Test]
  public async Task ShouldCreateNote() {
    // Current user
    var userId = await RunAsDefaultUserAsync();

    const string content = """
                           12.10 - doctor 9.30 AM
                           18.10 - dentist 12 PM
                           """;

    var cmd = new CreateNoteCommand("Appointments", content, userId, NoteBackground.Green);

    var result = await SendAsync(cmd);

    var createdNote = await FindAsync<Note>(result);

    createdNote.Should().NotBeNull();
    createdNote!.OwnerId.Should().Be(userId);
    createdNote.Content.Should().Be(content);
  }
}
