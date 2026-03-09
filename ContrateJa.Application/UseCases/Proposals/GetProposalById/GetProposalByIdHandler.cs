using ContrateJa.Application.Abstractions.Repositories;

namespace ContrateJa.Application.UseCases.Proposals.GetProposalById;

public sealed class GetProposalByIdHandler
{
  private readonly IProposalRepository _proposalRepository;

  public GetProposalByIdHandler(IProposalRepository proposalRepository)
    => _proposalRepository = proposalRepository;

  public async Task Execute(GetProposalByIdQuery query, CancellationToken ct = default)
  {
    if (query is null)
      throw new ArgumentNullException(nameof(query));

    if (query.ProposalId <= 0)
      throw new ArgumentOutOfRangeException(nameof(query.ProposalId));

    var proposal = await _proposalRepository.GetById(query.ProposalId, ct);

    if (proposal is null)
      throw new InvalidOperationException("Proposal not found.");
  }
}