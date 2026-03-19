using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.ValueObjects;
using ContrateJa.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContrateJa.Infrastructure.Repositories;

public sealed class FreelancerAreaRepository : IFreelancerAreaRepository
{
    private readonly AppDbContext _context;

    public FreelancerAreaRepository(AppDbContext context)
        => _context = context;

    public async Task<FreelancerArea?> GetById(long id, CancellationToken ct = default)
        => await _context.FreelancerAreas.FindAsync([id], ct);

    public async Task<IReadOnlyList<FreelancerArea>> ListByFreelancerId(long freelancerId, CancellationToken ct = default)
        => await _context.FreelancerAreas.Where(x => x.FreelancerId == freelancerId).ToListAsync(ct);

    public async Task<bool> Exists(long freelancerId, Area area, CancellationToken ct = default)
        => await _context.FreelancerAreas.AnyAsync(x =>
            x.FreelancerId == freelancerId &&
            x.Area.State.Code == area.State.Code &&
            x.Area.City.Name == area.City.Name, ct);

    public async Task Add(FreelancerArea freelancerArea, CancellationToken ct = default)
        => await _context.FreelancerAreas.AddAsync(freelancerArea, ct);

    public Task Remove(FreelancerArea freelancerArea, CancellationToken ct = default)
    {
        _context.FreelancerAreas.Remove(freelancerArea);
        return Task.CompletedTask;
    }
}