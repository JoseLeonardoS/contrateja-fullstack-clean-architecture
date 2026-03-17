using MediatR;

namespace ContrateJa.Application.UseCases.Proposals.AcceptProposal;

public sealed class AcceptProposalCommand : IRequest
{
  public long ProposalId { get; }

  public AcceptProposalCommand(long proposalId)
    => ProposalId = proposalId > 0 ? proposalId : throw new ArgumentOutOfRangeException(nameof(proposalId));
}