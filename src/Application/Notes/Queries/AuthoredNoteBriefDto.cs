using neatbook.Domain.Notes;
using neatbook.Domain.Notes.Enums;

namespace neatbook.Application.Notes.Queries;

public class AuthoredNoteBriefDto {
  public int Id { get; set; }
  public string Title { get; set; }
  public string Content { get; set; }
  public NoteBackground Background { get; set; }
  public List<string> Labels { get; set; } = new();

  public static AuthoredNoteBriefDto MapFromNote(Note note) {
    var dto = new AuthoredNoteBriefDto(note.Id, note.Title, note.Content, note.Background) {
      Labels = note.Labels.Select(l => l.Name).ToList()
    };
    return dto;
  }

  private AuthoredNoteBriefDto(int id, string title, string content, NoteBackground background) {
    Id = id;
    Title = title;
    Content = content;
    Background = background;
  }
}
