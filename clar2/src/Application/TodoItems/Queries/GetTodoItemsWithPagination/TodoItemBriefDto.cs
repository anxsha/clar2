using clar2.Application.Common.Mappings;
using clar2.Domain.ToDoItems;
using clar2.Domain.TodoLists;

namespace clar2.Application.TodoItems.Queries.GetTodoItemsWithPagination;

public class TodoItemBriefDto : IMapFrom<TodoItem> {
  public int Id { get; set; }

  public int ListId { get; set; }

  public string? Title { get; set; }

  public bool Done { get; set; }
}
