using ContrateJa.Domain.Enums;

namespace ContrateJa.Application.UseCases.Jobs.UpdateJobStatus;

public sealed class UpdateJobStatusCommand
{
    public long JobId { get; }
    public EJobStatus NewStatus { get; }

    public UpdateJobStatusCommand(long jobId, EJobStatus newStatus)
    {
        JobId = jobId;
        NewStatus = newStatus;
    }
}