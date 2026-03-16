using FluentValidation;

namespace ContrateJa.Application.UseCases.Jobs.UpdateJobTitle;

public sealed class UpdateJobTitleCommandValidator : AbstractValidator<UpdateJobTitleCommand>
{
    public UpdateJobTitleCommandValidator()
    {
        RuleFor(job => job.NewTitle)
            .NotEmpty()
            .MaximumLength(150);
    }
}