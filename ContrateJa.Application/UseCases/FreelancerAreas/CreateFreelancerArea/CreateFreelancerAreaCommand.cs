using MediatR;

namespace ContrateJa.Application.UseCases.FreelancerAreas.CreateFreelancerArea;

public sealed class CreateFreelancerAreaCommand : IRequest
{
    public long FreelancerId { get; }
    public string State { get; }
    public string City { get; }

    public CreateFreelancerAreaCommand(long freelancerId, string state, string city)
    {
        FreelancerId = freelancerId > 0 ? freelancerId 
            : throw new ArgumentOutOfRangeException(nameof(freelancerId));
        State = state;
        City =  city;
    }
}