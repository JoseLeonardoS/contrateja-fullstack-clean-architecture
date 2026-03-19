using FluentValidation;

namespace ContrateJa.Application.UseCases.Reviews.EditReview;

public sealed class EditReviewCommandValidator : AbstractValidator<EditReviewCommand>
{
    public EditReviewCommandValidator()
    {
        RuleFor(review => review.Rating)
            .NotEmpty()
            .GreaterThan(0)
            .LessThanOrEqualTo(5);
    }
}