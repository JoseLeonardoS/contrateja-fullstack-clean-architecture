using ContrateJa.Domain.Enums;
using MediatR;

namespace ContrateJa.Application.UseCases.Jobs.UpdateJobStatus;

public sealed class UpdateJobStatusCommand : IRequest
{
    public long JobId { get; }
    public string NewStatus { get; }

    public UpdateJobStatusCommand(long jobId, string newStatus)
    {
        JobId = jobId > 0 ? jobId : throw new ArgumentOutOfRangeException(nameof(jobId));
        NewStatus = newStatus;
    }
}