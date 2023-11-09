namespace neatbook.Domain.Notes.Events; 

public class NoteCreatedEvent : BaseEvent {
  public NoteCreatedEvent(Note createdNote) {
    CreatedNote = createdNote;
  }
  public Note CreatedNote { get; }
}
