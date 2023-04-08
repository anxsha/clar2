using clar2.Application.Common.Mappings;
using clar2.Domain.TodoLists;

namespace clar2.Application.TodoLists.Queries.GetTodos;

public class TodoListDto : IMapFrom<TodoList> {
  public TodoListDto() {
    Items = new List<TodoItemDto>();
  }

  public int Id { get; set; }

  public string? Title { get; set; }

  public string? Colour { get; set; }

  public IList<TodoItemDto> Items { get; set; }
}
