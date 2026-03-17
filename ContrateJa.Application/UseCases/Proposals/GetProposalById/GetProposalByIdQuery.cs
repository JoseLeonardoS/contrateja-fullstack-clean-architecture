using ContrateJa.Application.UseCases.Proposals.Shared;
using MediatR;

namespace ContrateJa.Application.UseCases.Proposals.GetProposalById;

public sealed class GetProposalByIdQuery : IRequest<ProposalResponse>
{
  public long ProposalId { get; }

  public GetProposalByIdQuery(long proposalId)
    => ProposalId = proposalId > 0 ? proposalId 
        : throw new ArgumentOutOfRangeException(nameof(proposalId));
}