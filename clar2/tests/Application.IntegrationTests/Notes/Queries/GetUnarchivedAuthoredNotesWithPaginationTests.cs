using clar2.Application.Notes.Queries.GetUnarchivedAuthoredNotesWithPagination;
using clar2.Domain.Notes;
using clar2.Domain.Notes.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace clar2.Application.IntegrationTests.Notes.Queries;

using static Testing;

public class GetUnarchivedAuthoredNotesWithPaginationTests : BaseTestFixture {
  [Test]
  public async Task ShouldReturnPaginatedNotesNotArchivedAndCreatedByCurrentUser() {
    
    var otherUser = await RunAsUserAsync("other@user", "Testing1234!", Array.Empty<string>());
    
    // Current user
    var userId = await RunAsDefaultUserAsync();

    // Generate current-user-created notes
    for (int i = 0; i < 14; i++) {
      var entity = new Note("Next week exams",
        """
                Monday – Math
                Tuesday – Physics
                Friday – Programming
                """,
        userId, NoteBackground.Red);
      entity.AddLabel("school");
      entity.AddLabel("education");
      entity.AddLabel("exams");
      entity.AddLabel("owned note");
      await AddAsync(entity);
    }
    
    // Generate current-user-collaborated notes which should not be returned by the query
    for (int i = 0; i < 5; i++) {
      var entity = new Note("Next week exams",
        """
                Monday – Math
                Tuesday – Physics
                Friday – Programming
                """,
        otherUser, NoteBackground.Green);
      entity.AddCollaborator(userId);
      entity.AddLabel("school");
      entity.AddLabel("education");
      entity.AddLabel("exams");
      await AddAsync(entity);
    }

    var query = new GetUnarchivedAuthoredNotesWithPaginationQuery(userId, 2, 5);

    var result = await SendAsync(query);

    result.TotalPages.Should().Be(3);
    result.HasNextPage.Should().Be(true);
    result.HasPreviousPage.Should().Be(true);
    result.TotalCount.Should().Be(14);
    result.Items.Should().HaveCount(5);
    result.Items.Should().AllSatisfy(n => n.Labels.Should().Contain("owned note"));

  }
}
