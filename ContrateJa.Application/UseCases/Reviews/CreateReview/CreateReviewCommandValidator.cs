using FluentValidation;

namespace ContrateJa.Application.UseCases.Reviews.CreateReview;

public sealed class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(review => review.Rating)
            .NotNull()
            .GreaterThan(0)
            .LessThanOrEqualTo(5);

        RuleFor(review => review.Comment)
            .NotNull()
            .MaximumLength(1000);
    }
}