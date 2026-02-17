using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.DTOs;

namespace ContrateJa.Application.UseCases.Jobs.GetJobById;

public sealed class GetJobByIdHandler
{
    private readonly IJobRepository _jobRepository;
    
    public GetJobByIdHandler(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<JobDto> Handle(GetJobByIdQuery query, CancellationToken ct = default)
    {
        if(query is null)
            throw new ArgumentNullException(nameof(query));

        if (query.Id <= 0)
            throw new ArgumentOutOfRangeException(nameof(query.Id));

        var job = await _jobRepository.GetById(query.Id, ct);
        
        if(job is null)
            throw new InvalidOperationException("Job not found.");

        return new JobDto(
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
            job.UpdatedAt);
    }
}