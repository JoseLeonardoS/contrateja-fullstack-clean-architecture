namespace ContrateJa.Application.UseCases.Jobs.UpdateJobTitle;

public sealed class UpdateJobTitleCommand
{
    public long JobId { get; }
    public string NewTitle { get; }

    public UpdateJobTitleCommand(long jobId, string newTitle)
    {
        JobId = jobId;
        NewTitle = newTitle;
    }
}