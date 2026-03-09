namespace ContrateJa.Application.Abstractions;

public interface IQueryHandler<TQuery, TResult>
{
    Task<TResult> Execute(TQuery query, CancellationToken ct = default);
}