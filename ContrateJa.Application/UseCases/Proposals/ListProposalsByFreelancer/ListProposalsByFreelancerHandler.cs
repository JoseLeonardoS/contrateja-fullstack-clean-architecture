using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.UseCases.Proposals.Shared;
using MediatR;

namespace ContrateJa.Application.UseCases.Proposals.ListProposalsByFreelancer;

public sealed class ListProposalsByFreelancerHandler : IRequestHandler<ListProposalsByFreelancerQuery, IReadOnlyList<ProposalResponse>>
{
  private readonly IProposalRepository _proposalRepository;

  public ListProposalsByFreelancerHandler(IProposalRepository proposalRepository)
    => _proposalRepository = proposalRepository;

  public async Task<IReadOnlyList<ProposalResponse>> Handle(ListProposalsByFreelancerQuery query, 
    CancellationToken ct = default)
  {
    var list = await _proposalRepository.GetAll(ct);
    
    return list.Select(proposal => new ProposalResponse(
      proposal.Id,
      proposal.JobId,
      proposal.FreelancerId,
      proposal.Money.Amount,
      proposal.Money.Currency,
      proposal.CoverLetter,
      proposal.Status.ToString(),
      proposal.CreatedAt,
      proposal.UpdatedAt,
      proposal.SubmittedAt))
      .ToList();
  }
}