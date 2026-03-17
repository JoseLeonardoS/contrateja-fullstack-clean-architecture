using ContrateJa.Domain.ValueObjects;
using MediatR;

namespace ContrateJa.Application.UseCases.Proposals.EditProposal;

public sealed  class EditProposalCommand : IRequest
{
    public long ProposalId { get; }
    public decimal Amount { get; }
    public string Currency { get; }
    public string CoverLetter { get;}

    public EditProposalCommand(long proposalId, decimal amount, string currency, string coverLetter)
    {
        ProposalId = proposalId > 0 ? proposalId : throw new ArgumentOutOfRangeException(nameof(proposalId));
        Amount = amount;
        Currency = currency;
        CoverLetter = coverLetter;
    }
}