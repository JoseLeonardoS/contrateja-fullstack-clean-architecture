using ContrateJa.Application.UseCases.Jobs.Shared;
using MediatR;

namespace ContrateJa.Application.UseCases.Jobs.GetJobById;

public sealed class GetJobByIdQuery : IRequest<JobResponse>
{
    public long Id { get; set; }
    
    public GetJobByIdQuery(long id)
        =>  Id = id > 0 ? id : throw new ArgumentOutOfRangeException(nameof(id));
}