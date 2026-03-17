using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Enums;
using ContrateJa.Domain.Exceptions;
using MediatR;

namespace ContrateJa.Application.UseCases.Proposals.RejectProposal;

public sealed class RejectProposalHandler : IRequestHandler<RejectProposalCommand>
{
  private readonly IProposalRepository _proposalRepository;
  private readonly IUnitOfWork _unitOfWork;

  public RejectProposalHandler(
    IProposalRepository proposalRepository,
    IUnitOfWork unitOfWork)
  {
    _proposalRepository = proposalRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task Handle(RejectProposalCommand command, CancellationToken ct = default)
  {
    var proposal = await _proposalRepository.GetById(command.ProposalId, ct);
    if (proposal is null)
      throw new NotFoundException(nameof(Proposal), command.ProposalId);

    proposal.EditStatus(EProposalStatus.Rejected);

    await _unitOfWork.SaveChanges(ct);
  }
}