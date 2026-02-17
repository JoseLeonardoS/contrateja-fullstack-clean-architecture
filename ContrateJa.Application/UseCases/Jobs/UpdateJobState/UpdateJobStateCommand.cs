using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Application.UseCases.Jobs.UpdateJobState;

public sealed class UpdateJobStateCommand
{
    public long JobId { get; }
    public State NewState { get; }
    
    public UpdateJobStateCommand(long jobId, State newState)
    {
        JobId = jobId;
        NewState = newState;
    }
}