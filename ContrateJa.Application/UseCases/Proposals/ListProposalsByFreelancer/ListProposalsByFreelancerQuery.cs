using ContrateJa.Application.UseCases.Proposals.Shared;
using MediatR;

namespace ContrateJa.Application.UseCases.Proposals.ListProposalsByFreelancer;

public sealed class ListProposalsByFreelancerQuery : IRequest<IReadOnlyList<ProposalResponse>>
{
  public long FreelancerId { get; }

  public ListProposalsByFreelancerQuery(long freelancerId)
    => FreelancerId = freelancerId > 0 ? freelancerId : throw new ArgumentOutOfRangeException(nameof(freelancerId));
}