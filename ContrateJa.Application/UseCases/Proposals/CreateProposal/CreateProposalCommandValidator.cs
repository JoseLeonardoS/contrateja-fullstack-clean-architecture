using FluentValidation;

namespace ContrateJa.Application.UseCases.Proposals.CreateProposal;

public sealed class CreateProposalCommandValidator : AbstractValidator<CreateProposalCommand>
{
    public CreateProposalCommandValidator()
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