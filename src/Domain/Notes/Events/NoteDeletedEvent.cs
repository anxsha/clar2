namespace neatbook.Domain.Notes.Events; 

public class NoteDeletedEvent : BaseEvent {
  public NoteDeletedEvent(Note deletedNote) {
    DeletedNote = deletedNote;
  }
  public Note DeletedNote { get; }
}
