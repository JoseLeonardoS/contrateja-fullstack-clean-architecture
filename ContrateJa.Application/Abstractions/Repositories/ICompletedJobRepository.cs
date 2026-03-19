using ContrateJa.Domain.Entities;

namespace ContrateJa.Application.Abstractions.Repositories;

public interface ICompletedJobRepository
{
    Task<IReadOnlyList<CompletedJob>> GetAll(CancellationToken ct = default);
    Task Add(CompletedJob completedJob, CancellationToken ct = default);
    Task<bool> ExistsById(long jobId, CancellationToken ct = default);
    Task<CompletedJob?> GetById(long jobId, CancellationToken ct = default);
    Task<IReadOnlyList<CompletedJob>> ListByFreelancerId(long freelancerId, CancellationToken ct = default);
}