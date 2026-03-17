using MediatR;

namespace ContrateJa.Application.UseCases.Proposals.RejectProposal;

public sealed class RejectProposalCommand : IRequest
{
  public long ProposalId { get; }

  public RejectProposalCommand(long proposalId)
    => ProposalId = proposalId > 0  ? proposalId 
        : throw new ArgumentOutOfRangeException(nameof(proposalId));
}