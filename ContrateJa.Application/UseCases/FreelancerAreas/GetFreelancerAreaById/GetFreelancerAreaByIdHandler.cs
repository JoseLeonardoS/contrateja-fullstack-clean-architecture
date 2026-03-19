using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.UseCases.FreelancerAreas.Shared;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Exceptions;
using MediatR;

namespace ContrateJa.Application.UseCases.FreelancerAreas.GetFreelancerAreaById;

public sealed class GetFreelancerAreaByIdHandler : IRequestHandler<GetFreelancerAreaByIdQuery, FreelancerAreaResponse>
{
    private readonly IFreelancerAreaRepository _freelancerAreaRepository;

    public GetFreelancerAreaByIdHandler(IFreelancerAreaRepository freelancerAreaRepository)
        => _freelancerAreaRepository = freelancerAreaRepository;

    public async Task<FreelancerAreaResponse> Handle(GetFreelancerAreaByIdQuery query, CancellationToken ct = default)
    {
        var area = await _freelancerAreaRepository.GetById(query.Id, ct);
        if (area is null)
            throw new NotFoundException(nameof(FreelancerArea), query.Id);

        return new FreelancerAreaResponse(
            area.Id,
            area.FreelancerId,
            area.Area.State.Code,
            area.Area.City.Name,
            area.CreatedAt);
    }
}