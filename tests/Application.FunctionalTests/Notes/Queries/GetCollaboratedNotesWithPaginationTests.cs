using neatbook.Application.Notes.Queries.GetCollaboratedNotesWithPagination;
using neatbook.Domain.Notes;
using neatbook.Domain.Notes.Enums;

namespace neatbook.Application.FunctionalTests.Notes.Queries; 

using static Testing;

public class GetCollaboratedNotesWithPaginationTests : BaseTestFixture {
  [Test]
  public async Task ShouldReturnPaginatedNotesOnlyWhereUserIsCollaborator() {
    // Two random note creators
    var noteOwner1Id = await RunAsUserAsync("note@owner1", "Testing1234!", Array.Empty<string>());
    var noteOwner2Id = await RunAsUserAsync("note@owner2", "Testing1234!", Array.Empty<string>());
    
    // Current user
    var userId = await RunAsDefaultUserAsync();

    var rnd = new Random();

    // Generate current-user-collaborated notes
    for (int i = 0; i < 14; i++) {
      // Set a random note creator and collaborator permissions for the current user
      var noteOwnerId = rnd.Next(2) == 0 ? noteOwner1Id : noteOwner2Id;
      var permissions = rnd.Next(2) == 0 ? CollaboratorPermissions.Read : CollaboratorPermissions.ReadWrite;
      
      var entity = new Note("Next week exams",
        """
                Monday – Math
                Tuesday – Physics
                Friday – Programming
                """,
        noteOwnerId, NoteBackground.Red);
      entity.AddCollaborator(userId, permissions);
      entity.AddLabel("school");
      entity.AddLabel("education");
      entity.AddLabel("exams");
      entity.AddLabel("collaborated");
      await AddAsync(entity);
    }
    
    // Generate current-user-created notes which should not be returned by the query
    for (int i = 0; i < 10; i++) {
      var entity = new Note("Next week exams",
        """
                Monday – Math
                Tuesday – Physics
                Friday – Programming
                """,
        userId, NoteBackground.Green);
      entity.AddLabel("school");
      entity.AddLabel("education");
      entity.AddLabel("exams");
      await AddAsync(entity);
    }

    var query = new GetCollaboratedNotesWithPaginationQuery(userId, 2, 5);

    var result = await SendAsync(query);

    result.TotalPages.Should().Be(3);
    result.HasNextPage.Should().Be(true);
    result.HasPreviousPage.Should().Be(true);
    result.TotalCount.Should().Be(14);
    result.Items.Should().HaveCount(5);
    result.Items.Should().AllSatisfy(n => n.Labels.Should().Contain("collaborated"));
  }
}
