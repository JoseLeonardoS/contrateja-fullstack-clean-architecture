using MediatR;

namespace ContrateJa.Application.UseCases.Jobs.UpdateJobTitle;

public sealed class UpdateJobTitleCommand : IRequest
{
    public long JobId { get; }
    public string NewTitle { get; }

    public UpdateJobTitleCommand(long jobId, string newTitle)
    {
        JobId = jobId > 0 ? jobId : throw new ArgumentOutOfRangeException(nameof(jobId));
        NewTitle = newTitle;
    }
}