using neatbook.Domain.Notes;
using neatbook.Domain.Notes.Enums;

namespace neatbook.Application.Notes.Queries.GetNoteById;

public class NoteDto {
  public int Id { get; private set; }
  public string Title { get; private set; }
  public string Content { get; private set; }
  public NoteBackground Background { get; private set; }
  public List<NotePictureDto> Pictures { get; private set; } = [];
  public string OwnerId { get; private set; }
  public List<NoteCollaboratorDto> Collaborators { get; private set; } = [];
  public List<string> Labels { get; private set; } = [];
  public bool IsArchived { get; private set; }

  public static NoteDto MapFromNote(Note note) {
    var dto = new NoteDto(note.Id, note.Title, note.Content, note.Background, note.OwnerId, note.IsArchived) {
      Labels = note.Labels.Select(l => l.Name).ToList(),
      Pictures = note.Pictures.Select(p => new NotePictureDto(p.Id, p.Url)).ToList(),
      Collaborators = note.Collaborators.Select(c => new NoteCollaboratorDto(c.CollaboratorId, c.Permissions)).ToList()
    };
    return dto;
  }

  private NoteDto(int id, string title, string content, NoteBackground background, string ownerId, bool isArchived) {
    Id = id;
    Title = title;
    Content = content;
    Background = background;
    OwnerId = ownerId;
    IsArchived = isArchived;
  }
  
  public bool UserCanView(string userId) {
    return OwnerId == userId || Collaborators.Any(c => c.CollaboratorId == userId);
  }
}

public class NotePictureDto {
  public int Id { get; private set; }
  public string Url { get; private set; }

  public NotePictureDto(int id, string url) {
    Id = id;
    Url = url;
  }
}

public class NoteCollaboratorDto {
  public string CollaboratorId { get; private set; }
  public CollaboratorPermissions Permissions { get; private set; }

  public NoteCollaboratorDto(string collaboratorId, CollaboratorPermissions permissions) {
    CollaboratorId = collaboratorId;
    Permissions = permissions;
  }
}
