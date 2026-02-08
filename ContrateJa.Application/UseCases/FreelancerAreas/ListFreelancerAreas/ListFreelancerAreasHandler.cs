using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.DTOs;

namespace ContrateJa.Application.UseCases.FreelancerAreas.ListFreelancerAreas;

public sealed class ListFreelancerAreasHandler
{
    private  readonly IFreelancerAreaRepository _freelancerAreaRepository;

    public ListFreelancerAreasHandler(
        IFreelancerAreaRepository freelancerAreaRepository)
    {
        _freelancerAreaRepository = freelancerAreaRepository;
    }

    public async Task<IReadOnlyList<FreelancerAreaDto>> Execute(
        ListFreelancerAreasQuery query, 
        CancellationToken ct = default)
    {
        if(query is null)
            throw new ArgumentNullException(nameof(query));
        
        if(query.FreelancerId <= 0)
            throw new ArgumentOutOfRangeException(nameof(query.FreelancerId));
        
        var freelancerAreas = await _freelancerAreaRepository
            .ListByFreelancerId(query.FreelancerId, ct);
        
        return freelancerAreas
            .Select(area => new FreelancerAreaDto(
                area.Id,
                area.FreelancerId,
                area.Area))
            .ToList();
    }
}