namespace ContrateJa.Application.UseCases.FreelancerAreas.RemoveFreelancerArea;

public sealed class RemoveFreelancerAreaCommand
{
    public long FreelancerAreaId { get; }

    public RemoveFreelancerAreaCommand(long freelancerAreaId)
     =>  FreelancerAreaId = freelancerAreaId;
}