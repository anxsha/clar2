using FluentValidation;

namespace clar2.Application.Notes.Commands.DeleteNote;

public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand> {
  public DeleteNoteCommandValidator() {
  }
}
