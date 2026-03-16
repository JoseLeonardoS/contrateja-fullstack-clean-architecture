using FluentValidation;

namespace ContrateJa.Application.UseCases.Jobs.UpdateJobDescription;

public sealed class UpdateJobDescriptionCommandValidator : AbstractValidator<UpdateJobDescriptionCommand>
{
    public UpdateJobDescriptionCommandValidator()
    {
        RuleFor(job => job.NewDescription)
            .NotEmpty()
            .MaximumLength(1000);
    }
}