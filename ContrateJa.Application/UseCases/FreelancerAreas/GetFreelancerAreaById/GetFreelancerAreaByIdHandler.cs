using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.DTOs;

namespace ContrateJa.Application.UseCases.FreelancerAreas.GetFreelancerAreaById;

public sealed class GetFreelancerAreaByIdHandler
{
    private readonly IFreelancerAreaRepository _freelancerAreaRepository;

    public GetFreelancerAreaByIdHandler(IFreelancerAreaRepository freelancerAreaRepository)
    {
        _freelancerAreaRepository = freelancerAreaRepository;
    }

    public async Task<FreelancerAreaDto> Execute(GetFreelancerAreaByIdQuery query, CancellationToken ct = default)
    {
        if(query.Id <= 0)
            throw new ArgumentOutOfRangeException(nameof(query.Id));
        
        var freelancerArea = await _freelancerAreaRepository.GetById(query.Id, ct);

        if (freelancerArea is null)
            throw new InvalidOperationException("Freelancer area not found.");

        return new FreelancerAreaDto(
            freelancerArea.Id, 
            freelancerArea.FreelancerId, 
            freelancerArea.Area);
    }
}