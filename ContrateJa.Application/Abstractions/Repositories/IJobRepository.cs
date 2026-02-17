using ContrateJa.Domain.Entities;

namespace ContrateJa.Application.Abstractions.Repositories;

public interface IJobRepository
{
    Task<IReadOnlyList<Job>> ListAll(CancellationToken ct = default);
    Task<IReadOnlyList<Job>> ListByContractorId(long contractorId,CancellationToken ct = default);
    Task<Job?> GetById(long id, CancellationToken ct = default);
    Task Add(Job job, CancellationToken ct = default);
    Task Remove(Job job, CancellationToken ct = default);
}