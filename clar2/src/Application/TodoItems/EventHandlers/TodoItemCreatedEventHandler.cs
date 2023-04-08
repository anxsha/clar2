using clar2.Domain.ToDoItems.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace clar2.Application.TodoItems.EventHandlers;

public class TodoItemCreatedEventHandler : INotificationHandler<TodoItemCreatedEvent> {
  private readonly ILogger<TodoItemCreatedEventHandler> _logger;

  public TodoItemCreatedEventHandler(ILogger<TodoItemCreatedEventHandler> logger) {
    _logger = logger;
  }

  public Task Handle(TodoItemCreatedEvent notification, CancellationToken cancellationToken) {
    _logger.LogInformation("clar2 Domain Event: {DomainEvent}", notification.GetType().Name);

    return Task.CompletedTask;
  }
}
