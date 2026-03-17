using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;
using ContrateJa.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContrateJa.Infrastructure.Repositories;

public sealed class ProposalRepository : IProposalRepository
{
    private readonly AppDbContext _context;
    
    public ProposalRepository(AppDbContext context)
        => _context = context;

    public async Task<IReadOnlyList<Proposal>> GetAll(CancellationToken ct = default)
        => await _context.Proposals.ToListAsync(ct);

    public async Task<IReadOnlyList<Proposal>> ListByJobId(long jobId, CancellationToken ct = default)
        => await _context.Proposals.Where(p => p.JobId == jobId).ToListAsync(ct);

    public async Task<IReadOnlyList<Proposal>> ListByFreelancerId(long freelancerId, CancellationToken ct = default)
        => await _context.Proposals.Where(p => p.FreelancerId == freelancerId).ToListAsync(ct);

    public async Task<Proposal?> GetById(long id, CancellationToken ct = default)
        =>  await _context.Proposals.FindAsync([id], ct);

    public async Task Add(Proposal proposal, CancellationToken ct = default)
        => await _context.Proposals.AddAsync(proposal, ct);

    public Task Remove(Proposal proposal, CancellationToken ct = default)
    {
        _context.Proposals.Remove(proposal);
        return Task.CompletedTask;
    }
}
