using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.UseCases.FreelancerAreas.Shared;
using MediatR;

namespace ContrateJa.Application.UseCases.FreelancerAreas.ListFreelancerAreas;

public sealed class ListFreelancerAreasHandler : IRequestHandler<ListFreelancerAreasQuery, IReadOnlyList<FreelancerAreaResponse>>
{
    private readonly IFreelancerAreaRepository _freelancerAreaRepository;

    public ListFreelancerAreasHandler(IFreelancerAreaRepository freelancerAreaRepository)
        => _freelancerAreaRepository = freelancerAreaRepository;

    public async Task<IReadOnlyList<FreelancerAreaResponse>> Handle(ListFreelancerAreasQuery query, CancellationToken ct = default)
    {
        var list = await _freelancerAreaRepository.ListByFreelancerId(query.FreelancerId, ct);

        return list.Select(x => new FreelancerAreaResponse(
            x.Id,
            x.FreelancerId,
            x.Area.State.Code,
            x.Area.City.Name,
            x.CreatedAt)).ToList();
    }
}