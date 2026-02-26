namespace ContrateJa.Application.UseCases.Proposals.AcceptProposal;

public sealed class AcceptProposalCommand
{
  public long ProposalId { get; }

  public AcceptProposalCommand(long proposalId)
    => ProposalId = proposalId;
}