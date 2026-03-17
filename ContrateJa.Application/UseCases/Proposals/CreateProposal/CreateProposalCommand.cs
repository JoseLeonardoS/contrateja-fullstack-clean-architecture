using MediatR;

namespace ContrateJa.Application.UseCases.Proposals.CreateProposal;

public sealed class CreateProposalCommand : IRequest
{
    public long JobId { get; }
    public long FreelancerId { get; }
    public decimal Amount { get; }
    public string Currency { get; }
    public string CoverLetter { get; }

    public CreateProposalCommand(
        long jobId, 
        long freelancerId,
        decimal amount, 
        string currency,
        string coverLetter)
    {
        JobId = jobId > 0 ? jobId : throw new ArgumentOutOfRangeException(nameof(jobId));
        FreelancerId = freelancerId > 0 ? freelancerId : throw new ArgumentOutOfRangeException(nameof(freelancerId));
        Amount = amount;
        Currency = currency;
        CoverLetter = coverLetter;
    }
}