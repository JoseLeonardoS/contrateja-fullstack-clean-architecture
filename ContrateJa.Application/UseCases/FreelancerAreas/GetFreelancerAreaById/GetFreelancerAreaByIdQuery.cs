using MediatR;
using ContrateJa.Application.UseCases.FreelancerAreas.Shared;

namespace ContrateJa.Application.UseCases.FreelancerAreas.GetFreelancerAreaById;

public sealed class GetFreelancerAreaByIdQuery(long id) : IRequest<FreelancerAreaResponse>
{
    public long Id { get; } = id > 0 ? id : throw new ArgumentOutOfRangeException(nameof(id));
}