namespace ContrateJa.Application.Abstractions.Repositories;

public interface IUnitOfWork
{
    Task SaveChanges(CancellationToken ct = default);
}