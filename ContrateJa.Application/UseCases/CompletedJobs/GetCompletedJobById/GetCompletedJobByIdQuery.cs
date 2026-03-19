using MediatR;
using ContrateJa.Application.UseCases.CompletedJobs.Shared;

namespace ContrateJa.Application.UseCases.CompletedJobs.GetCompletedJobById;

public sealed class GetCompletedJobByIdQuery(long id) : IRequest<CompletedJobResponse>
{
    public long Id { get; } = id > 0 ? id 
        : throw new ArgumentOutOfRangeException(nameof(id));
}