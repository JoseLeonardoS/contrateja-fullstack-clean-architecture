using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.UseCases.Jobs.Shared;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Exceptions;
using MediatR;

namespace ContrateJa.Application.UseCases.Jobs.GetJobById;

public sealed class GetJobByIdHandler : IRequestHandler<GetJobByIdQuery, JobResponse>
{
    private readonly IJobRepository _jobRepository;
    
    public GetJobByIdHandler(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<JobResponse> Handle(GetJobByIdQuery query, CancellationToken ct = default)
    {
        var job = await _jobRepository.GetById(query.Id, ct);
        if(job is null)
            throw new NotFoundException(nameof(Job), query.Id);

        return new JobResponse(
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
            job.UpdatedAt);
    }
}