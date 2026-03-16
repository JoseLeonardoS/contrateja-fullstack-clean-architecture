using ContrateJa.Domain.Enums;
using FluentValidation;

namespace ContrateJa.Application.UseCases.Jobs.UpdateJobStatus;

public sealed class UpdateJobStatusCommandValidator : AbstractValidator<UpdateJobStatusCommand>
{
    public UpdateJobStatusCommandValidator()
    {
        RuleFor(job => job.NewStatus)
            .NotEmpty()
            .Must(x => Enum.TryParse<EJobStatus>(x, true, out _))
            .WithMessage("Invalid job status.");
    }
}