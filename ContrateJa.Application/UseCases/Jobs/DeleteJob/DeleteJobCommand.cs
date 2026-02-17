namespace ContrateJa.Application.UseCases.Jobs.DeleteJob;

public sealed class DeleteJobCommand
{
    public long JobId { get; }
    
    public DeleteJobCommand(long jobId)
        =>  JobId = jobId;
}