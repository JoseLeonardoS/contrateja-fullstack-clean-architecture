namespace ContrateJa.Application.UseCases.Jobs.UpdateJobDescription;

public sealed class UpdateJobDescriptionCommand
{
    public long JobId { get; }
    public string NewDescription { get; }

    public UpdateJobDescriptionCommand(long jobId, string newDescription)
    {
        JobId = jobId;
        NewDescription =  newDescription; 
    }
}