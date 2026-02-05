namespace ContrateJa.Application.UseCases.CompletedJobs.GetCompletedJobById;

public sealed class GetCompletedJobByIdQuery
{
    public long Id { get; set; }

    public GetCompletedJobByIdQuery(long id)
    {
        Id = id;
    }
}