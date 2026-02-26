using ContrateJa.Domain.Entities;
using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Application.Abstractions.Repositories;

public interface IFreelancerAreaRepository
{
    Task<FreelancerArea?> GetById(long id, CancellationToken ct = default);
    Task<IReadOnlyList<FreelancerArea>> ListByFreelancerId(long freelancerId, CancellationToken ct = default);
    Task<bool> Exists(long freelancerId, Area area, CancellationToken ct = default);
    Task Add(FreelancerArea freelancerArea, CancellationToken ct = default);
    Task Remove(FreelancerArea freelancerArea, CancellationToken ct = default);
} 