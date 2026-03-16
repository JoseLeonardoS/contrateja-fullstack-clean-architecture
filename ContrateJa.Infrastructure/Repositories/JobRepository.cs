using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;
using ContrateJa.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContrateJa.Infrastructure.Repositories;

public sealed class JobRepository : IJobRepository
{
    private readonly AppDbContext _context;

    public JobRepository(AppDbContext context)
        => _context = context;
    
    public async Task<IReadOnlyList<Job>> ListAll(CancellationToken ct = default)
        => await _context.Jobs.ToListAsync(ct);

    public async Task<IReadOnlyList<Job>> ListByContractorId(long contractorId, CancellationToken ct = default)
        => await  _context.Jobs.Where(x=>x.ContractorId == contractorId).ToListAsync(ct);

    public async Task<Job?> GetById(long id, CancellationToken ct = default)
        => await  _context.Jobs.FindAsync([id], ct);

    public async Task Add(Job job, CancellationToken ct = default)
        => await  _context.Jobs.AddAsync(job, ct);

    public Task Remove(Job job, CancellationToken ct = default)
    {
        _context.Jobs.Remove(job);
        return Task.CompletedTask;
    }
}