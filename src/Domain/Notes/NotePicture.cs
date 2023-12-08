namespace neatbook.Domain.Notes; 

public class NotePicture : BaseAuditableEntity {
  public string Url { get; private set; }
  public int NoteId { get; private set; }

  public NotePicture(string url, int noteId) {
    Url = url;
    NoteId = noteId;
  }
}
