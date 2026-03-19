using FluentValidation;

namespace ContrateJa.Application.UseCases.FreelancerAreas.CreateFreelancerArea;

public sealed class CreateFreelancerAreaCommandValidator : AbstractValidator<CreateFreelancerAreaCommand>
{
    public CreateFreelancerAreaCommandValidator()
    {
        RuleFor(x => x.State)
            .NotEmpty()
            .Length(2);

        RuleFor(x => x.City)
            .NotEmpty()
            .MaximumLength(100);
    }
}