using FluentValidation;

namespace ContrateJa.Application.UseCases.Users.UpdateUserAddress;

public sealed class UpdateUserAddressCommandValidator : AbstractValidator<UpdateUserAddressCommand>
{
    public UpdateUserAddressCommandValidator()
    {
        RuleFor(x => x.NewState)
            .NotEmpty()
            .Length(2);
        
        RuleFor(x => x.NewCity)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(100);

        RuleFor(x => x.NewStreet)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.NewZipCode)
            .NotEmpty()
            .Length(8);
    }
}