namespace ContrateJa.Application.UseCases.Proposals.GetProposalById;

public sealed class GetProposalByIdQuery
{
  public long ProposalId { get; }

  public GetProposalByIdQuery(long proposalId)
    => ProposalId = proposalId;
}