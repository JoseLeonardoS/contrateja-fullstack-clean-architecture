using ContrateJa.Domain.Entities;
using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Application.Abstractions.Repositories;

public interface IUserRepository
{
    Task<IReadOnlyList<User>> ListAll(int page, int pageSize, CancellationToken ct = default);
    Task<User?> GetById(long userId, CancellationToken ct = default);
    Task<User?> GetByEmail(Email email, CancellationToken ct = default);
    Task<bool> ExistsByEmail(Email email, CancellationToken ct = default);
    Task Add(User user, CancellationToken ct = default);
    Task Remove(User user, CancellationToken ct = default);
}