using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;
using ContrateJa.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContrateJa.Infrastructure.Repositories;

public sealed class CompletedJobRepository : ICompletedJobRepository
{
    private readonly AppDbContext _context;

    public CompletedJobRepository(AppDbContext context)
        => _context = context;

    public async Task<IReadOnlyList<CompletedJob>> GetAll(CancellationToken ct = default)
        => await _context.CompletedJobs.ToListAsync(ct);

    public async Task<CompletedJob?> GetById(long id, CancellationToken ct = default)
        => await _context.CompletedJobs.FindAsync([id], ct);

    public async Task<IReadOnlyList<CompletedJob>> ListByFreelancerId(long freelancerId, CancellationToken ct = default)
        => await _context.CompletedJobs.Where(x => x.FreelancerId == freelancerId).ToListAsync(ct);
    
    public async Task<bool> ExistsById(long jobId, CancellationToken ct = default)
        => await _context.CompletedJobs.AnyAsync(x => x.JobId == jobId, ct);

    public async Task Add(CompletedJob completedJob, CancellationToken ct = default)
        => await _context.CompletedJobs.AddAsync(completedJob, ct);
}