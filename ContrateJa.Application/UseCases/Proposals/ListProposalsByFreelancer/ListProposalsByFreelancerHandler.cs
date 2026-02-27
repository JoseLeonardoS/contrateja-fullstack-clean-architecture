using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.DTOs;

namespace ContrateJa.Application.UseCases.Proposals.ListProposalsByFreelancer;

public sealed class ListProposalsByFreelancerHandler
{
  private readonly IProposalRepository _proposalRepository;

  public ListProposalsByFreelancerHandler(IProposalRepository proposalRepository)
    => _proposalRepository = proposalRepository;

  public async Task<IReadOnlyList<ProposalDto>> Execute(ListProposalsByFreelancerQuery query, CancellationToken ct = default)
  {
    if (query is null)
      throw new ArgumentNullException(nameof(query));

    if (query.FreelancerId <= 0)
      throw new ArgumentOutOfRangeException(nameof(query.FreelancerId));

    var list = await _proposalRepository.ListByFreelancerId(query.FreelancerId, ct);

    return list.Select(proposal => new ProposalDto(
      proposal.Id,
      proposal.JobId,
      proposal.FreelancerId,
      proposal.Amount,
      proposal.CoverLetter,
      proposal.Status,
      proposal.SubmittedAt,
      proposal.UpdatedAt))
      .ToList();
  }
}