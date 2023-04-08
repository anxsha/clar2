using clar2.Domain.ToDoItems;
using clar2.Domain.TodoLists.ValueObjects;

namespace clar2.Domain.TodoLists;

public class TodoList : BaseAuditableEntity {
  public string? Title { get; set; }

  public Colour Colour { get; set; } = Colour.White;

  public IList<TodoItem> Items { get; private set; } = new List<TodoItem>();
}
