using clar2.Application.Common.Interfaces;
using clar2.Domain.ToDoItems;
using clar2.Domain.ToDoItems.Events;
using clar2.Domain.TodoLists;
using MediatR;

namespace clar2.Application.TodoItems.Commands.CreateTodoItem;

public record CreateTodoItemCommand : IRequest<int> {
  public int ListId { get; init; }

  public string? Title { get; init; }
}

public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, int> {
  private readonly IApplicationDbContext _context;

  public CreateTodoItemCommandHandler(IApplicationDbContext context) {
    _context = context;
  }

  public async Task<int> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken) {
    var entity = new TodoItem {
      ListId = request.ListId,
      Title = request.Title,
      Done = false
    };

    entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

    _context.TodoItems.Add(entity);

    await _context.SaveChangesAsync(cancellationToken);

    return entity.Id;
  }
}
