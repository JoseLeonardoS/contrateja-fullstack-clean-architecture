using ContrateJa.Application.Abstractions.Repositories;

namespace ContrateJa.Application.UseCases.CompletedJobs.ListCompletedJobs;

public sealed class ListCompletedJobsHandler
{
    private readonly ICompletedJobRepository _completedJobRepository;

    public ListCompletedJobsHandler(ICompletedJobRepository completedJobRepository)
    {
        _completedJobRepository = completedJobRepository;
    }

    public async Task Execute(
        ListCompletedJobsQuery query,
        CancellationToken ct = default)
    {
        if (query is null)
            throw new ArgumentNullException(nameof(query));

        if (query.FreelancerId <= 0)
            throw new ArgumentOutOfRangeException(nameof(query.FreelancerId));
        
        var completedJobs = await _completedJobRepository
            .ListByFreelancerId(query.FreelancerId, ct);
    }
}