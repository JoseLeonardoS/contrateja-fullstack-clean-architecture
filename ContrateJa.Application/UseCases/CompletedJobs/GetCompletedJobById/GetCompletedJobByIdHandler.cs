using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.UseCases.CompletedJobs.Shared;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Exceptions;
using MediatR;

namespace ContrateJa.Application.UseCases.CompletedJobs.GetCompletedJobById;

public sealed class GetCompletedJobByIdHandler : IRequestHandler<GetCompletedJobByIdQuery, CompletedJobResponse>
{
    private readonly ICompletedJobRepository _completedJobRepository;

    public GetCompletedJobByIdHandler(ICompletedJobRepository completedJobRepository)
        => _completedJobRepository = completedJobRepository;

    public async Task<CompletedJobResponse> Handle(GetCompletedJobByIdQuery query, CancellationToken ct = default)
    {
        var completedJob = await _completedJobRepository.GetById(query.Id, ct);
        if (completedJob is null)
            throw new NotFoundException(nameof(CompletedJob), query.Id);

        return new CompletedJobResponse(
            completedJob.Id,
            completedJob.JobId,
            completedJob.FreelancerId,
            completedJob.ContractorId,
            completedJob.CompletedAt,
            completedJob.CreatedAt);
    }
}