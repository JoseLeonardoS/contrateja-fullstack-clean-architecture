using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.DTOs;

namespace ContrateJa.Application.UseCases.Jobs.ListJobsByContractor;

public sealed class ListJobsByContractorHandler
{
    private readonly IJobRepository _jobRepository;

    public ListJobsByContractorHandler(
        IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<IReadOnlyList<JobDto>> Execute(
        ListJobsByContractorQuery query, 
        CancellationToken ct = default)
    {
        if (query is null)
            throw new ArgumentNullException(nameof(query));

        if (query.ContractorId <= 0)
            throw new ArgumentOutOfRangeException(nameof(query.ContractorId));

        var list = await _jobRepository.ListByContractorId(query.ContractorId, ct);

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
                job.UpdatedAt))
            .ToList();
    }
}