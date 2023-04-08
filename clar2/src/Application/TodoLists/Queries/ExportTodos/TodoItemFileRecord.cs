using clar2.Application.Common.Mappings;
using clar2.Domain.ToDoItems;
using clar2.Domain.TodoLists;

namespace clar2.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem> {
  public string? Title { get; set; }

  public bool Done { get; set; }
}
