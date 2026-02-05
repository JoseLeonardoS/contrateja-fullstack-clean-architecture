using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.DTOs;

namespace ContrateJa.Application.UseCases.CompletedJobs.GetCompletedJobById;

public sealed class GetCompletedJobByIdHandler
{
    private readonly ICompletedJobRepository _completedJobRepository;

    public GetCompletedJobByIdHandler(ICompletedJobRepository completedJobRepository)
    {
         _completedJobRepository = completedJobRepository;
    }

    public async Task<CompletedJobDto> Execute(GetCompletedJobByIdQuery query, CancellationToken ct = default)
    {
        if (query.Id <= 0)
            throw new ArgumentOutOfRangeException(nameof(query.Id));
        
        var completedJob = await _completedJobRepository.GetById(query.Id, ct);
        
        if (completedJob == null)
            throw new InvalidOperationException("Completed job not found.");

        return new CompletedJobDto(
            completedJob.Id, 
            completedJob.JobId, 
            completedJob.FreelancerId, 
            completedJob.ContractorId,
            completedJob.CompletedAt);
    }
}