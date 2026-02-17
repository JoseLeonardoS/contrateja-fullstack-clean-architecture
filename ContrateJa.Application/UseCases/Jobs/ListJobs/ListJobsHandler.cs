using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.DTOs;

namespace ContrateJa.Application.UseCases.Jobs.ListJobs;

public sealed class ListJobsHandler
{
    private readonly IJobRepository _jobRepository;

    public ListJobsHandler(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<IReadOnlyList<JobDto>> Execute(
        ListJobsQuery query,
        CancellationToken ct = default)
    {
        var list =  await _jobRepository.ListAll(ct);
        
        return list.Select(job => new JobDto(
            job.Id,
            job.ContractorId,
            job.Title,
            job.Description,
            job.State,
            job.City,
            job.Street,
            job.ZipCode,
            job.Status,
            job.CreatedAt,
            job.UpdatedAt)).ToList();
    }
}