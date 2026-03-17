using FluentValidation;

namespace ContrateJa.Application.UseCases.Proposals.EditProposalCoverLetter;

public sealed class EditProposalCoverLetterCommandValidator : AbstractValidator<EditProposalCoverLetterCommand>
{
    public EditProposalCoverLetterCommandValidator()
    {
        RuleFor(command => command.CoverLetter)
            .NotNull()
            .MaximumLength(1000);
    }
}