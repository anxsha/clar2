using System.Globalization;
using clar2.Application.Common.Interfaces;
using clar2.Application.TodoLists.Queries.ExportTodos;
using clar2.Infrastructure.Files.Maps;
using CsvHelper;

namespace clar2.Infrastructure.Files;

public class CsvFileBuilder : ICsvFileBuilder {
  public byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records) {
    using var memoryStream = new MemoryStream();
    using (var streamWriter = new StreamWriter(memoryStream)) {
      using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

      csvWriter.Context.RegisterClassMap<TodoItemRecordMap>();
      csvWriter.WriteRecords(records);
    }

    return memoryStream.ToArray();
  }
}
