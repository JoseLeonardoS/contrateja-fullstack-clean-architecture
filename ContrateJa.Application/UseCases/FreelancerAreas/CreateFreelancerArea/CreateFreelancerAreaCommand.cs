using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Application.UseCases.FreelancerAreas.CreateFreelancerArea;

public sealed class CreateFreelancerAreaCommand
{
    public long FreelancerId { get; }
    public Area Area { get; }

    public CreateFreelancerAreaCommand(long freelancerId, Area area)
    {
        FreelancerId = freelancerId;
        Area = area;
    }
}