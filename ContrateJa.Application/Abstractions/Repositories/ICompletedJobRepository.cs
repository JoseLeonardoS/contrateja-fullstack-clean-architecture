using ContrateJa.Domain.Entities;

namespace ContrateJa.Application.Abstractions.Repositories;

public interface ICompletedJobRepository
{
    Task Add(CompletedJob completedJob, CancellationToken ct = default);
    Task<bool> ExistsByJobId(long jobId, CancellationToken ct = default);
    Task<CompletedJob> GetById(long jobId, CancellationToken ct = default);
    Task<IReadOnlyList<CompletedJob>> ListByFreelancerId(long freelancerId, CancellationToken ct = default);
}