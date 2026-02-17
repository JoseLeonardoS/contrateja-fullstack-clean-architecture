using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Application.UseCases.Jobs.UpdateJobZipCode;

public sealed class UpdateJobZipCodeCommand
{
    public long JobId { get; }
    public ZipCode NewZipCode { get; }

    public UpdateJobZipCodeCommand(long jobId, ZipCode newZipCode)
    {
        JobId = jobId;
        NewZipCode = newZipCode;
    }
}