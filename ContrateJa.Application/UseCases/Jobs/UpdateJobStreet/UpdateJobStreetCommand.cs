using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Application.UseCases.Jobs.UpdateJobStreet;

public sealed class UpdateJobStreetCommand
{
    public long JobId { get; }
    public Street NewStreet { get; }

    public UpdateJobStreetCommand(long jobId, Street newStreet)
    {
        JobId = jobId;
        NewStreet = newStreet;
    }
}