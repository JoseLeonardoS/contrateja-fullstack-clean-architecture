namespace ContrateJa.Application.UseCases.Jobs.GetJobById;

public sealed class GetJobByIdQuery
{
    public long Id { get; set; }
    
    public GetJobByIdQuery(long id)
        =>  Id = id;
}