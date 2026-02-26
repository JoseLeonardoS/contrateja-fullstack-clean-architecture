namespace ContrateJa.Application.UseCases.Proposals.DeleteProposal;

public sealed class DeleteProposalCommand
{
    public long ProposalId { get; }
    
    public DeleteProposalCommand(long proposalId)
        =>  ProposalId = proposalId;
}