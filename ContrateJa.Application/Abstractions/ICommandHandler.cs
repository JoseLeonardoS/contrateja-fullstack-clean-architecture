namespace ContrateJa.Application.Abstractions;

public interface ICommandHandler<TCommand>
{
    Task Execute(TCommand command, CancellationToken ct = default);
}

public interface ICommandHandler<TCommand, TResult>
{
    Task<TResult> Execute(TCommand command, CancellationToken ct = default);
}