using ContrateJa.Application.UseCases.Proposals.Shared;
using MediatR;

namespace ContrateJa.Application.UseCases.Proposals.ListProposalsByJob;

public sealed class ListProposalsByJobQuery : IRequest<IReadOnlyList<ProposalResponse>>
{
  public long JobId { get; }

  public ListProposalsByJobQuery(long jobId)
    => JobId = jobId > 0 ? jobId 
        : throw new ArgumentOutOfRangeException(nameof(jobId));
}