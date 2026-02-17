using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Application.UseCases.Jobs.UpdateJobCity;

public sealed class UpdateJobCityCommand
{
    public long JobId { get; }
    public City NewCity { get; }

    public UpdateJobCityCommand(long jobId, City newCity)
    {
        JobId = jobId;
        NewCity = newCity;
    }
}