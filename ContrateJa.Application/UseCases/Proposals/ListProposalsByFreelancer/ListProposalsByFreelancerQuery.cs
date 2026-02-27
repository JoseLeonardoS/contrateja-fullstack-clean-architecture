namespace ContrateJa.Application.UseCases.Proposals.ListProposalsByFreelancer;

public sealed class ListProposalsByFreelancerQuery
{
  public long FreelancerId { get; }

  public ListProposalsByFreelancerQuery(long freelancerId)
    => FreelancerId = freelancerId;
}