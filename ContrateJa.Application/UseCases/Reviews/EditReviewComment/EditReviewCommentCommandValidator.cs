using FluentValidation;

namespace ContrateJa.Application.UseCases.Reviews.EditReviewComment;

public sealed class EditReviewCommentCommandValidator : AbstractValidator<EditReviewCommentCommand>
{
    public EditReviewCommentCommandValidator()
    {
        RuleFor(review => review.Comment)
            .NotNull()
            .MaximumLength(1000);
    }
}