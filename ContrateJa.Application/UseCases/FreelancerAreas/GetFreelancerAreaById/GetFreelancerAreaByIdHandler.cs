using ContrateJa.Application.Abstractions.Repositories;

namespace ContrateJa.Application.UseCases.FreelancerAreas.GetFreelancerAreaById;

public sealed class GetFreelancerAreaByIdHandler
{
    private readonly IFreelancerAreaRepository _freelancerAreaRepository;

    public GetFreelancerAreaByIdHandler(IFreelancerAreaRepository freelancerAreaRepository)
    {
        _freelancerAreaRepository = freelancerAreaRepository;
    }

    public async Task Execute(GetFreelancerAreaByIdQuery query, CancellationToken ct = default)
    {
        if(query.Id <= 0)
            throw new ArgumentOutOfRangeException(nameof(query.Id));
        
        var freelancerArea = await _freelancerAreaRepository.GetById(query.Id, ct);

        if (freelancerArea is null)
            throw new InvalidOperationException("Freelancer area not found.");
    }
}