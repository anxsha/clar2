using FluentValidation;

namespace clar2.Application.Notes.Commands.EditNote;

public class EditNoteCommandValidator : AbstractValidator<EditNoteCommand> {
  public EditNoteCommandValidator() {
    RuleFor(v => v.Title)
      .MaximumLength(200)
      .NotEmpty();
    RuleFor(v => v.Content)
      .NotEmpty();
  }
}
