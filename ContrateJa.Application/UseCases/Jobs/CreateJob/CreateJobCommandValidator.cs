using FluentValidation;

namespace ContrateJa.Application.UseCases.Jobs.CreateJob;

public sealed class CreateJobCommandValidator : AbstractValidator<CreateJobCommand>
{
    public CreateJobCommandValidator()
    {
        RuleFor(job => job.Title)
            .NotEmpty()
            .MaximumLength(150);
        
        RuleFor(job => job.Description)
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(job => job.State)
            .NotEmpty()
            .Length(2);
        
        RuleFor(job => job.City)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(100);
        
        RuleFor(job => job.Street)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(150);

        RuleFor(job => job.ZipCode)
            .NotEmpty()
            .Length(8);
    }
}