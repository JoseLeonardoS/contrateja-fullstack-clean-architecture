using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Application.UseCases.Proposals.CreateProposal;

public sealed class CreateProposalCommand
{
    public long JobId { get; }
    public long FreelancerId { get; }
    public Money Amount { get; }
    public string CoverLetter { get; }

    public CreateProposalCommand(
        long jobId, 
        long freelancerId, 
        Money amount, 
        string coverLetter)
    {
        JobId = jobId;
        FreelancerId = freelancerId;
        Amount = amount;
        CoverLetter = coverLetter;
    }
}