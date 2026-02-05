namespace ContrateJa.Application.UseCases.CompletedJobs.CompleteJob;

public sealed class CompletedJobCommand
{
    public long JobId { get; }
    public long FreelancerId { get; }
    public long ContractorId { get; }

    public CompletedJobCommand(long jobId, long freelancerId, long contractorId)
    {
        JobId = jobId;
        FreelancerId = freelancerId;
        ContractorId = contractorId;
    }
}