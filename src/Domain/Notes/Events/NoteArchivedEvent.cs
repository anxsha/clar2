namespace neatbook.Domain.Notes.Events; 

public class NoteArchivedEvent : BaseEvent{
  public NoteArchivedEvent(Note archivedNote) {
    ArchivedNote = archivedNote;
  }
  public Note ArchivedNote { get; }
}
