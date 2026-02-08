using ContrateJa.Domain.Entities;
using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Application.UseCases.FreelancerAreas.UpdateFreelancerAreaCity;

public sealed class UpdateFreelancerAreaCityCommand
{
    public long FreelancerAreaId { get; }
    public City NewCity { get; }

    public UpdateFreelancerAreaCityCommand(long freelancerAreaId, City newCity)
    {
        FreelancerAreaId = freelancerAreaId;
        NewCity = newCity;
    }
}