using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Application.UseCases.Proposals.EditProposal;

public sealed  class EditProposalCommand
{
    public long ProposalId { get; }
    public Money NewAmount { get; }
    public string NewCoverLetter { get;}

    public EditProposalCommand(long proposalId, Money newAmount, string coverLetter)
    {
        ProposalId = proposalId;
        NewAmount = newAmount;
        NewCoverLetter = coverLetter;
    }
}