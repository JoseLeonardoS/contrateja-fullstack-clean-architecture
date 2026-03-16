using MediatR;

namespace ContrateJa.Application.UseCases.Jobs.CloseJob;

public sealed class CloseJobCommand :  IRequest
{
    public long JobId { get; }

    public CloseJobCommand(long jobId)
        =>  JobId = jobId;
}