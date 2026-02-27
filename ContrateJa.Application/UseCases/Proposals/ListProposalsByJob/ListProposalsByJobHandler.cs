using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.DTOs;

namespace ContrateJa.Application.UseCases.Proposals.ListProposalsByJob;

public sealed class ListProposalsByJobHandler
{
  private readonly IProposalRepository _proposalRepository;

  public ListProposalsByJobHandler(IProposalRepository proposalRepository)
    => _proposalRepository = proposalRepository;

  public async Task<IReadOnlyList<ProposalDto>> Execute(ListProposalsByJobQuery query, CancellationToken ct = default)
  {
    if (query is null)
      throw new ArgumentNullException(nameof(query));

    if (query.JobId <= 0)
      throw new ArgumentOutOfRangeException(nameof(query.JobId));

    var list = await _proposalRepository.ListByJobId(query.JobId, ct);

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