using FluentValidation;

namespace clar2.Application.Notes.Queries.GetUnarchivedAuthoredNotesWithPagination;

public class
  GetUnarchivedAuthoredNotesWithPaginationQueryValidator : AbstractValidator<
    GetUnarchivedAuthoredNotesWithPagination.GetUnarchivedAuthoredNotesWithPaginationQuery> {
  public GetUnarchivedAuthoredNotesWithPaginationQueryValidator() {
    RuleFor(x => x.PageNumber)
      .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

    RuleFor(x => x.PageSize)
      .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
  }
}
