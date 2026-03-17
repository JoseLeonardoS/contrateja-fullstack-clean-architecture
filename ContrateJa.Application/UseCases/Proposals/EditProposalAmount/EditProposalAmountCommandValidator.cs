using FluentValidation;

namespace ContrateJa.Application.UseCases.Proposals.EditProposalAmount;

public sealed class EditProposalAmountCommandValidator : AbstractValidator<EditProposalAmountCommand>
{
    public EditProposalAmountCommandValidator()
    {
        RuleFor(x => x.Amount)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.Currency)
            .NotEmpty()
            .Length(3);
    }
}