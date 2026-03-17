using MediatR;

namespace ContrateJa.Application.UseCases.Proposals.EditProposalCoverLetter;

public sealed class EditProposalCoverLetterCommand :  IRequest
{
  public long ProposalId { get; }
  public string CoverLetter { get; }

  public EditProposalCoverLetterCommand(long proposalId, string coverLetter)
  {
    ProposalId = proposalId > 0 ? proposalId : throw new ArgumentOutOfRangeException(nameof(proposalId));
    CoverLetter = coverLetter;
  }
}