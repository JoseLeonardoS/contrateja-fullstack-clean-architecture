using FluentValidation;

namespace ContrateJa.Application.UseCases.Reviews.ChangeReviewRating;

public sealed class ChangeReviewRatingCommandValidator : AbstractValidator<ChangeReviewRatingCommand>
{
    public ChangeReviewRatingCommandValidator()
    {
        RuleFor(command => command.Rating)
            .NotEmpty()
            .GreaterThan(0)
            .LessThanOrEqualTo(5);
    }
}