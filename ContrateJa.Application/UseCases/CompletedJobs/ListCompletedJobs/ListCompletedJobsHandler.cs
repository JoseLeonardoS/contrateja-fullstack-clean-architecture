using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.UseCases.CompletedJobs.Shared;
using MediatR;

namespace ContrateJa.Application.UseCases.CompletedJobs.ListCompletedJobs;

public sealed class ListCompletedJobsHandler : IRequestHandler<ListCompletedJobsQuery, IReadOnlyList<CompletedJobResponse>>
{
    private readonly ICompletedJobRepository _completedJobRepository;

    public ListCompletedJobsHandler(ICompletedJobRepository completedJobRepository)
        => _completedJobRepository = completedJobRepository;

    public async Task<IReadOnlyList<CompletedJobResponse>> Handle(ListCompletedJobsQuery query, CancellationToken ct = default)
    {
        var list = await _completedJobRepository.GetAll(ct);

        return list.Select(x => new CompletedJobResponse(
            x.Id,
            x.JobId,
            x.FreelancerId,
            x.ContractorId,
            x.CompletedAt,
            x.CreatedAt)).ToList();
    }
}