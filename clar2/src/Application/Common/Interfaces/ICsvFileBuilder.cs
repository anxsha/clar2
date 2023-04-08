using clar2.Application.TodoLists.Queries.ExportTodos;

namespace clar2.Application.Common.Interfaces;

public interface ICsvFileBuilder {
  byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
