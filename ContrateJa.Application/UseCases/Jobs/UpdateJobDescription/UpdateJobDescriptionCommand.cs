using MediatR;

namespace ContrateJa.Application.UseCases.Jobs.UpdateJobDescription;

public sealed class UpdateJobDescriptionCommand : IRequest
{
    public long JobId { get; }
    public string NewDescription { get; }

    public UpdateJobDescriptionCommand(long jobId, string newDescription)
    {
        JobId = jobId > 0 ? jobId : throw new ArgumentOutOfRangeException(nameof(jobId));
        NewDescription =  newDescription; 
    }
}