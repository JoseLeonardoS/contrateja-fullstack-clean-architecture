using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.UseCases.Proposals.Shared;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Exceptions;
using MediatR;

namespace ContrateJa.Application.UseCases.Proposals.GetProposalById;

public sealed class GetProposalByIdHandler : IRequestHandler<GetProposalByIdQuery, ProposalResponse>
{
  private readonly IProposalRepository _proposalRepository;

  public GetProposalByIdHandler(IProposalRepository proposalRepository)
    => _proposalRepository = proposalRepository;

  public async Task<ProposalResponse> Handle(GetProposalByIdQuery query, CancellationToken ct = default)
  {
    var proposal = await _proposalRepository.GetById(query.ProposalId, ct);
    if (proposal is null)
      throw new NotFoundException(nameof(Proposal), query.ProposalId);
    
    return new ProposalResponse(
      proposal.Id,
      proposal.JobId,
      proposal.FreelancerId,
      proposal.Money.Amount,
      proposal.Money.Currency,
      proposal.CoverLetter,
      proposal.Status.ToString(),
      proposal.CreatedAt,
      proposal.UpdatedAt,
      proposal.SubmittedAt);
  }
}