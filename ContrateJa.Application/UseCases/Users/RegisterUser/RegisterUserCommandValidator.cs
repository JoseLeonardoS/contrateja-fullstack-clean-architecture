using ContrateJa.Domain.Enums;
using FluentValidation;

namespace ContrateJa.Application.UseCases.Users.RegisterUser;

public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(75);
        
        RuleFor(x => x.LastName)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(75);
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(255);
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(64);
        
        RuleFor(x => x.CountryCode)
            .NotEmpty()
            .Must(x => Enum.TryParse<ECountryCode>(x, true, out _))
            .WithMessage("Invalid country code.");

        RuleFor(x => x.Phone)
            .NotEmpty()
            .Matches(@"^\d+$")
            .WithMessage("Phone must contain only digits.")
            .MinimumLength(6)
            .MaximumLength(20);
        
        RuleFor(x => x.Document)
            .NotEmpty()
            .Must(x => x.Length is 11 or 14)
            .WithMessage("Document must be a valid CPF (11 digits) or CNPJ (14 digits).");
        
        RuleFor(x => x.AccountType)
            .NotEmpty()
            .Must(x => Enum.TryParse<EAccountType>(x, true, out _))
            .WithMessage("Invalid account type.");

        RuleFor(x => x.State)
            .NotEmpty()
            .Length(2);

        RuleFor(x => x.City)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(100);

        RuleFor(x => x.Street)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.ZipCode)
            .NotEmpty()
            .Length(8);
    }
}