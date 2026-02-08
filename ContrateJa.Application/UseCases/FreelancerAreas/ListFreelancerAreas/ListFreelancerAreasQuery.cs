namespace ContrateJa.Application.UseCases.FreelancerAreas.ListFreelancerAreas;

public sealed class ListFreelancerAreasQuery
{
    public long FreelancerId { get; set; }

    public ListFreelancerAreasQuery(long freelancerId)
    {
        FreelancerId = freelancerId;
    }
}