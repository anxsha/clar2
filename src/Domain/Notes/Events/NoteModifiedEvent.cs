namespace neatbook.Domain.Notes.Events; 

public class NoteModifiedEvent : BaseEvent {
  public NoteModifiedEvent(Note modifiedNote) {
    ModifiedNote = modifiedNote;
  }
  public Note ModifiedNote { get; }
}
