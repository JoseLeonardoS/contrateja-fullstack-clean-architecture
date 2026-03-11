using ContrateJa.Application.Abstractions.Repositories;

namespace ContrateJa.Infrastructure.Persistence;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;
    
    public UnitOfWork(AppDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task SaveChanges(CancellationToken ct = default)
        => await _dbContext.SaveChangesAsync(ct);
}