using ContrateJa.Domain.Enums;
using FluentValidation;

namespace ContrateJa.Application.UseCases.Users.UpdateUserPhone;

public sealed class UpdateUserPhoneCommandValidator : AbstractValidator<UpdateUserPhoneCommand>
{
    public UpdateUserPhoneCommandValidator()
    {
        RuleFor(x => x.Phone)
            .NotEmpty()
            .Matches(@"^\d+$")
            .MinimumLength(6)
            .MaximumLength(20);

        RuleFor(x => x.CountryCode)
            .NotEmpty()
            .Must( x => Enum.TryParse<ECountryCode>(x, true, out _))
            .WithMessage("Invalid country code.");
    }
}