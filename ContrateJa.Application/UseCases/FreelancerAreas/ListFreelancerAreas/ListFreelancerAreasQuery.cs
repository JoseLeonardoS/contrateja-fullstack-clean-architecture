using MediatR;
using ContrateJa.Application.UseCases.FreelancerAreas.Shared;

namespace ContrateJa.Application.UseCases.FreelancerAreas.ListFreelancerAreas;

public sealed class ListFreelancerAreasQuery(long freelancerId) : IRequest<IReadOnlyList<FreelancerAreaResponse>>
{
    public long FreelancerId { get; } = freelancerId > 0 ? freelancerId : throw new ArgumentOutOfRangeException(nameof(freelancerId));
}