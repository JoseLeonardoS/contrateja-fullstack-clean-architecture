using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Application.UseCases.Proposals.EditProposalAmount;

public sealed class EditProposalAmountCommand
{
  public long ProposalId { get; }
  public Money NewAmount { get; }

  public EditProposalAmountCommand(long proposalId, Money newAmount)
  {
    ProposalId = proposalId;
    NewAmount = newAmount;
  }
}