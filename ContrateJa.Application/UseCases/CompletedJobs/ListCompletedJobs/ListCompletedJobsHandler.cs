using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.DTOs;
using ContrateJa.Domain.Entities;

namespace ContrateJa.Application.UseCases.CompletedJobs.ListCompletedJobs;

public sealed class ListCompletedJobsHandler
{
    private readonly ICompletedJobRepository _completedJobRepository;

    public ListCompletedJobsHandler(ICompletedJobRepository completedJobRepository)
    {
        _completedJobRepository = completedJobRepository;
    }

    public async Task<IReadOnlyList<CompletedJobDto>> Execute(
        ListCompletedJobsQuery query,
        CancellationToken ct = default)
    {
        var completedJobs = await _completedJobRepository
            .ListByFreelancerId(query.FreelancerId, ct);
        
        return completedJobs
            .Select(job => new CompletedJobDto(
                job.Id,
                job.JobId,
                job.FreelancerId,
                job.ContractorId,
                job.CompletedAt))
            .ToList();
    }
}