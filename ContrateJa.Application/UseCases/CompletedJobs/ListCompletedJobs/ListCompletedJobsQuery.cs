namespace ContrateJa.Application.UseCases.CompletedJobs.ListCompletedJobs;

public sealed class ListCompletedJobsQuery
{
    public long FreelancerId { get; }

    public ListCompletedJobsQuery(long freelancerId)
    {
        FreelancerId = freelancerId;
    }
}