using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.UseCases.Jobs.Shared;
using MediatR;

namespace ContrateJa.Application.UseCases.Jobs.ListJobs;

public sealed class ListJobsHandler : IRequestHandler<ListJobsQuery,IReadOnlyList<JobResponse>>
{
    private readonly IJobRepository _jobRepository;

    public ListJobsHandler(IJobRepository jobRepository)
        => _jobRepository = jobRepository;

    public async Task<IReadOnlyList<JobResponse>> Handle(ListJobsQuery query, CancellationToken ct = default)
    {
        var list = await _jobRepository.ListAll(ct);

        return list.Select(job => new JobResponse(
            job.Id,
            job.ContractorId,
            job.Title,
            job.Description,
            job.State.Code,
            job.City.Name,
            job.Street.Name,
            job.ZipCode.Value,
            job.Status.ToString(),
            job.CreatedAt,
            job.UpdatedAt))
            .ToList();
    }
}