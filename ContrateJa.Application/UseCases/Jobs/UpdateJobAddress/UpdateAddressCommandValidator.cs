using FluentValidation;

namespace ContrateJa.Application.UseCases.Jobs.UpdateJobAddress;

public sealed class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
{
    public UpdateAddressCommandValidator()
    {
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