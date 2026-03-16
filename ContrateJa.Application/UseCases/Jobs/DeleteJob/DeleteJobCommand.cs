using MediatR;

namespace ContrateJa.Application.UseCases.Jobs.DeleteJob;

public sealed class DeleteJobCommand : IRequest
{
    public long JobId { get; }
    
    public DeleteJobCommand(long jobId)
        =>  JobId = jobId > 0 ? jobId : throw new ArgumentOutOfRangeException(nameof(jobId));
}