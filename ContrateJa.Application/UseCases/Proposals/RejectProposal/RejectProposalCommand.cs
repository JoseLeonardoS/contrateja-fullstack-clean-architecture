namespace ContrateJa.Application.UseCases.Proposals.RejectProposal;

public sealed class RejectProposalCommand
{
  public long ProposalId { get; }

  public RejectProposalCommand(long proposalId)
  => ProposalId = proposalId;
}