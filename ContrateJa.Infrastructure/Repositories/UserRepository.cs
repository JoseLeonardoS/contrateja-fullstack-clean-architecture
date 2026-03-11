using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.ValueObjects;
using ContrateJa.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContrateJa.Infrastructure.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
        => _context = context;

    public async Task<IReadOnlyList<User>> ListAll(int page, int pageSize, CancellationToken ct = default)
        => await _context.Users
            .OrderBy(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

    public async Task<User?> GetById(long userId, CancellationToken ct = default)
        => await _context.Users.FindAsync([userId], ct);

    public async Task<User?> GetByEmail(Email email, CancellationToken ct = default)
        => await _context.Users.FirstOrDefaultAsync(x => x.Email.Address == email.Address, ct);

    public async Task<bool> ExistsByEmail(Email email, CancellationToken ct = default)
        => await _context.Users.AnyAsync(x => x.Email.Address == email.Address, ct);

    public async Task Add(User user, CancellationToken ct = default)
        => await _context.Users.AddAsync(user, ct);

    public async Task Remove(User user, CancellationToken ct = default)
    {
        _context.Users.Remove(user);
        await Task.CompletedTask;
    }
}