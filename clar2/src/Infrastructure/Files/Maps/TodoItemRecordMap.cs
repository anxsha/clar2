using System.Globalization;
using clar2.Application.TodoLists.Queries.ExportTodos;
using CsvHelper.Configuration;

namespace clar2.Infrastructure.Files.Maps;

public class TodoItemRecordMap : ClassMap<TodoItemRecord> {
  public TodoItemRecordMap() {
    AutoMap(CultureInfo.InvariantCulture);

    Map(m => m.Done).Convert(c => c.Value.Done ? "Yes" : "No");
  }
}