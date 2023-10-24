using FluentValidation;

namespace clar2.Application.Notes.Commands.CreateNote;

public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand> {
  public CreateNoteCommandValidator() {
    RuleFor(v => v.Title)
      .MaximumLength(200)
      .NotEmpty();
    RuleFor(v => v.Content)
      .NotEmpty();
    RuleFor(v => v.Background)
      .NotEmpty();
    RuleFor(v => v.UserId)
      .NotEmpty();
  }
}
