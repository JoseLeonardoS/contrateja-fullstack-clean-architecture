using ContrateJa.Application.Abstractions.Repositories;

namespace ContrateJa.Application.UseCases.Jobs.ListJobs;

public sealed class ListJobsHandler
{
    private readonly IJobRepository _jobRepository;

    public ListJobsHandler(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task Execute(
        ListJobsQuery query,
        CancellationToken ct = default)
    {
        var list =  await _jobRepository.ListAll(ct);
    }
}