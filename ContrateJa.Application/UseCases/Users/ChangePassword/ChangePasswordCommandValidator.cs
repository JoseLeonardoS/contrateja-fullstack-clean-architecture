using FluentValidation;

namespace ContrateJa.Application.UseCases.Users.ChangePassword;

public sealed class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor( x => x.OldPassword)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(64);
        
        RuleFor( x => x.NewPassword)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(64);
        
        RuleFor( x => x.ConfirmPassword)
            .NotEmpty()
            .Equal(x => x.NewPassword)
            .WithMessage("Passwords do not match.");
    }
}