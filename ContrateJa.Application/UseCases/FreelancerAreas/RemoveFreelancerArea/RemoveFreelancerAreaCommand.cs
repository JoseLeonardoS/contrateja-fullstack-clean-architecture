using MediatR;

namespace ContrateJa.Application.UseCases.FreelancerAreas.RemoveFreelancerArea;

public sealed class RemoveFreelancerAreaCommand : IRequest
{
    public long FreelancerAreaId { get; }

    public RemoveFreelancerAreaCommand(long freelancerAreaId)
     =>  FreelancerAreaId = freelancerAreaId > 0 ? freelancerAreaId 
         : throw new ArgumentOutOfRangeException(nameof(freelancerAreaId));
}