namespace clar2.Domain.ToDoItems.Events;

public class TodoItemCompletedEvent : BaseEvent {
  public TodoItemCompletedEvent(TodoItem item) {
    Item = item;
  }

  public TodoItem Item { get; }
}
