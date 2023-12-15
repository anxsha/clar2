namespace neatbook.Domain.Notes.Events; 

public class NoteUnarchivedEvent : BaseEvent{
  public NoteUnarchivedEvent(Note unarchivedNote) {
    UnarchivedNote = unarchivedNote;
  }
  public Note UnarchivedNote { get; }
}
