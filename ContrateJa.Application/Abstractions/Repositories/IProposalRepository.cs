using ContrateJa.Domain.Entities;

namespace ContrateJa.Application.Abstractions.Repositories;

public interface IProposalRepository
{
    Task<IReadOnlyList<Proposal>> GetAll(CancellationToken ct = default);
    Task<IReadOnlyList<Proposal>> ListByJobId(long jobId, CancellationToken ct = default);
    Task<IReadOnlyList<Proposal>> GetByFreelancerId(long freelancerId, CancellationToken ct = default);
    Task<Proposal?> GetById(long id, CancellationToken ct = default);
    Task Add(Proposal proposal, CancellationToken ct = default);
    Task Remove(Proposal proposal, CancellationToken ct = default);
}
