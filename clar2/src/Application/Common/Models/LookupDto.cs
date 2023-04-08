using clar2.Application.Common.Mappings;
using clar2.Domain.ToDoItems;
using clar2.Domain.TodoLists;

namespace clar2.Application.Common.Models;

// Note: This is currently just used to demonstrate applying multiple IMapFrom attributes.
public class LookupDto : IMapFrom<TodoList>, IMapFrom<TodoItem> {
  public int Id { get; set; }

  public string? Title { get; set; }
}
