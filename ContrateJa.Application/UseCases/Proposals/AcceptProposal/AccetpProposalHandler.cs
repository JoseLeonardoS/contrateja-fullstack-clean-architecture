using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Enums;

namespace ContrateJa.Application.UseCases.Proposals.AcceptProposal;

public sealed class AcceptProposalHandler
{
  private readonly IProposalRepository _proposalRepository;
  private readonly IUnitOfWork _unitOfWork;

  public AcceptProposalHandler(
    IProposalRepository proposalRepository,
    IUnitOfWork unitOfWork)
  {
    _proposalRepository = proposalRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task Execute(AcceptProposalCommand command, CancellationToken ct = default)
  {
    if (command is null)
      throw new ArgumentNullException(nameof(command));

    if (command.ProposalId <= 0)
      throw new ArgumentOutOfRangeException(nameof(command.ProposalId));

    var proposal = await _proposalRepository.GetById(command.ProposalId, ct);

    if (proposal is null)
      throw new InvalidOperationException("Proposal not found.");

    proposal.EditStatus(EProposalStatus.Accepted);

    await _unitOfWork.SaveChanges(ct);
  }
}
