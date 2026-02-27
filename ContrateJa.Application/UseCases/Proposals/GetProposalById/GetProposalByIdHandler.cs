using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.DTOs;

namespace ContrateJa.Application.UseCases.Proposals.GetProposalById;

public sealed class GetProposalByIdHandler
{
  private readonly IProposalRepository _proposalRepository;

  public GetProposalByIdHandler(IProposalRepository proposalRepository)
    => _proposalRepository = proposalRepository;

  public async Task<ProposalDto> Execute(GetProposalByIdQuery query, CancellationToken ct = default)
  {
    if (query is null)
      throw new ArgumentNullException(nameof(query));

    if (query.ProposalId <= 0)
      throw new ArgumentOutOfRangeException(nameof(query.ProposalId));

    var proposal = await _proposalRepository.GetById(query.ProposalId, ct);

    if (proposal is null)
      throw new InvalidOperationException("Proposal not found.");

    return new ProposalDto(
      proposal.Id,
      proposal.JobId,
      proposal.FreelancerId,
      proposal.Amount,
      proposal.CoverLetter,
      proposal.Status,
      proposal.SubmittedAt,
      proposal.UpdatedAt);
  }
}