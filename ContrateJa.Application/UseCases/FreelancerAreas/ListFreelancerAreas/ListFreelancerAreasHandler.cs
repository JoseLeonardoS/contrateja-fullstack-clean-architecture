using ContrateJa.Application.Abstractions.Repositories;

namespace ContrateJa.Application.UseCases.FreelancerAreas.ListFreelancerAreas;

public sealed class ListFreelancerAreasHandler
{
    private  readonly IFreelancerAreaRepository _freelancerAreaRepository;

    public ListFreelancerAreasHandler(
        IFreelancerAreaRepository freelancerAreaRepository)
    {
        _freelancerAreaRepository = freelancerAreaRepository;
    }

    public async Task Execute(
        ListFreelancerAreasQuery query, 
        CancellationToken ct = default)
    {
        if(query is null)
            throw new ArgumentNullException(nameof(query));
        
        if(query.FreelancerId <= 0)
            throw new ArgumentOutOfRangeException(nameof(query.FreelancerId));
        
        var freelancerAreas = await _freelancerAreaRepository
            .ListByFreelancerId(query.FreelancerId, ct);
    }
}