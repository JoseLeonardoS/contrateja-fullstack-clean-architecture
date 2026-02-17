namespace ContrateJa.Application.UseCases.Jobs.CloseJob;

public sealed class CloseJobCommand
{
    public long JobId { get; }

    public CloseJobCommand(long jobId)
        =>  JobId = jobId;
}