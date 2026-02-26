namespace ContrateJa.Application.UseCases.Proposals.EditProposalCoverLetter;

public sealed class EditProposalCoverLetterCommand
{
  public long ProposalId { get; }
  public string NewCoverLetter { get; }

  public EditProposalCoverLetterCommand(long proposalId, string newCoverLetter)
  {
    ProposalId = proposalId;
    NewCoverLetter = newCoverLetter;
  }
}