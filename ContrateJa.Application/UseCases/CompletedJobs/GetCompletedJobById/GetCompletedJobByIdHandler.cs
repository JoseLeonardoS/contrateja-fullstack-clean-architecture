using ContrateJa.Application.Abstractions.Repositories;

namespace ContrateJa.Application.UseCases.CompletedJobs.GetCompletedJobById;

public sealed class GetCompletedJobByIdHandler
{
    private readonly ICompletedJobRepository _completedJobRepository;

    public GetCompletedJobByIdHandler(ICompletedJobRepository completedJobRepository)
    {
         _completedJobRepository = completedJobRepository;
    }

    public async Task Execute(GetCompletedJobByIdQuery query, CancellationToken ct = default)
    {
        if (query.Id <= 0)
            throw new ArgumentOutOfRangeException(nameof(query.Id));
        
        var completedJob = await _completedJobRepository.GetById(query.Id, ct);
        
        if (completedJob == null)
            throw new InvalidOperationException("Completed job not found.");
    }
}