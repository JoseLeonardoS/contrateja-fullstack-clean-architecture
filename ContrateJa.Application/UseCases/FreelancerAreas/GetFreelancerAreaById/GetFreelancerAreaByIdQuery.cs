namespace ContrateJa.Application.UseCases.FreelancerAreas.GetFreelancerAreaById;

public sealed class GetFreelancerAreaByIdQuery
{
    public long Id { get; }
    
    public GetFreelancerAreaByIdQuery(long id)
    {
        Id = id;
    }
}
