using MediatR;

namespace ContrateJa.Application.UseCases.CompletedJobs.CompleteJob;

public sealed class CompleteJobCommand : IRequest
{
    public long JobId { get; }
    public long FreelancerId { get; }
    public long ContractorId { get; }

    public CompleteJobCommand(long jobId, long freelancerId, long contractorId)
    {
        JobId = jobId > 0 ? jobId : throw new ArgumentOutOfRangeException(nameof(jobId));
        FreelancerId = freelancerId > 0 ? freelancerId : throw new ArgumentOutOfRangeException(nameof(freelancerId));
        ContractorId = contractorId > 0 ? contractorId : throw new ArgumentOutOfRangeException(nameof(contractorId));
    }
}