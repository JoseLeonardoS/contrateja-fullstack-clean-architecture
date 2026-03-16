using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.UseCases.Jobs.Shared;
using MediatR;

namespace ContrateJa.Application.UseCases.Jobs.ListJobsByContractor;

public sealed class ListJobsByContractorHandler : IRequestHandler<ListJobsByContractorQuery, IReadOnlyList<JobResponse>>
{
    private readonly IJobRepository _jobRepository;

    public ListJobsByContractorHandler(IJobRepository jobRepository)
        => _jobRepository = jobRepository;

    public async Task<IReadOnlyList<JobResponse>> Handle(ListJobsByContractorQuery query, CancellationToken ct = default)
    {
        var list = await _jobRepository.ListByContractorId(query.ContractorId, ct);
        
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