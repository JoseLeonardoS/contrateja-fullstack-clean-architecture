using ContrateJa.Application.Abstractions.Repositories;

namespace ContrateJa.Application.UseCases.Proposals.EditProposalAmount;

public sealed class EditProposalAmountHandler
{
  private readonly IProposalRepository _proposalRepository;
  private readonly IUnitOfWork _unitOfWork;

  public EditProposalAmountHandler(
    IProposalRepository proposalRepository,
    IUnitOfWork unitOfWork)
  {
    _proposalRepository = proposalRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task Execute(EditProposalAmountCommand command, CancellationToken ct = default)
  {
    if (command is null)
      throw new ArgumentNullException(nameof(command));

    if (command.ProposalId <= 0)
      throw new ArgumentOutOfRangeException(nameof(command.ProposalId));

    var proposal = await _proposalRepository.GetById(command.ProposalId, ct);

    if (proposal is null)
      throw new InvalidOperationException("Proposal not found.");

    proposal.EditAmount(command.NewAmount);

    await _unitOfWork.SaveChanges(ct);
  }
}