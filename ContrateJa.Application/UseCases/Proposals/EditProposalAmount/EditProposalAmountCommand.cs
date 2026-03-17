using MediatR;

namespace ContrateJa.Application.UseCases.Proposals.EditProposalAmount;

public sealed class EditProposalAmountCommand : IRequest
{
  public long ProposalId { get; }
  public decimal Amount { get; }
  public string Currency { get; }

  public EditProposalAmountCommand(long proposalId, decimal amount, string currency)
  {
    ProposalId = proposalId > 0 ? proposalId : throw new ArgumentOutOfRangeException(nameof(proposalId));
    Amount = amount;
    Currency = currency;
  }
}