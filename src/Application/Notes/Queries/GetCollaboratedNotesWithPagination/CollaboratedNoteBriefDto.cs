using neatbook.Domain.Notes;
using neatbook.Domain.Notes.Enums;

namespace neatbook.Application.Notes.Queries.GetCollaboratedNotesWithPagination;

public class CollaboratedNoteBriefDto {
  public int Id { get; set; }
  public string Title { get; set; }
  public string Content { get; set; }
  public NoteBackground Background { get; set; }
  public bool IsArchived { get; set; }
  public List<string> Labels { get; set; } = new();
  public CollaboratorPermissions Permissions { get; set; }

  public static CollaboratedNoteBriefDto MapFromNote(Note note, string currentUserId) {
    var dto = new CollaboratedNoteBriefDto(note.Id, note.Title, note.Content, note.Background, note.IsArchived) {
      Labels = note.Labels.Select(l => l.Name).ToList(),
      Permissions = note.Collaborators
        .Where(c => c.CollaboratorId == currentUserId)
        .Select(p => p.Permissions).FirstOrDefault()
    };
    return dto;
  }

  private CollaboratedNoteBriefDto(int id, string title, string content, NoteBackground background, bool isArchived) {
    Id = id;
    Title = title;
    Content = content;
    Background = background;
    IsArchived = isArchived;
  }
}
