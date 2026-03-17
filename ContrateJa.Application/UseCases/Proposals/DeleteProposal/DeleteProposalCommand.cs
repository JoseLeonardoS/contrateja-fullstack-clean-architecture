using MediatR;

namespace ContrateJa.Application.UseCases.Proposals.DeleteProposal;

public sealed class DeleteProposalCommand : IRequest
{
    public long ProposalId { get; }
    
    public DeleteProposalCommand(long proposalId)
        =>  ProposalId = proposalId > 0 ? proposalId 
            : throw new ArgumentOutOfRangeException(nameof(proposalId));
}