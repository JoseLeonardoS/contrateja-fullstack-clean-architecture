using FluentValidation;

namespace ContrateJa.Application.UseCases.Proposals.EditProposal;

public sealed class EditProposalCommandValidator : AbstractValidator<EditProposalCommand>
{
    public EditProposalCommandValidator()
    {
        RuleFor(x => x.Amount)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.Currency)
            .NotEmpty()
            .Length(3);

        RuleFor(x => x.CoverLetter)
            .NotEmpty()
            .MaximumLength(1000);
    }
}