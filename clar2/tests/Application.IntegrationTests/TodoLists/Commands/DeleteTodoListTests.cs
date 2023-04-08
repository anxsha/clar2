using clar2.Application.Common.Exceptions;
using clar2.Application.TodoLists.Commands.CreateTodoList;
using clar2.Application.TodoLists.Commands.DeleteTodoList;
using clar2.Domain.TodoLists;
using FluentAssertions;
using NUnit.Framework;

namespace clar2.Application.IntegrationTests.TodoLists.Commands;

using static Testing;

public class DeleteTodoListTests : BaseTestFixture {
  [Test]
  public async Task ShouldRequireValidTodoListId() {
    var command = new DeleteTodoListCommand(99);
    await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
  }

  [Test]
  public async Task ShouldDeleteTodoList() {
    var listId = await SendAsync(new CreateTodoListCommand {
      Title = "New List"
    });

    await SendAsync(new DeleteTodoListCommand(listId));

    var list = await FindAsync<TodoList>(listId);

    list.Should().BeNull();
  }
}
