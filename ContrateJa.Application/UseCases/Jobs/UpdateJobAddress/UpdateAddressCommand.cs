using MediatR;

namespace ContrateJa.Application.UseCases.Jobs.UpdateJobAddress;

public sealed class UpdateAddressCommand : IRequest
{
    public long JobId { get; }
    public string State { get; }
    public string City { get; }
    public string Street { get; }
    public string ZipCode { get; }
    
    public UpdateAddressCommand(long jobId, string state, string city, string street, string zipCode)
    {
        JobId = jobId > 0 ? jobId : throw new ArgumentOutOfRangeException(nameof(jobId));
        State = state;
        City = city;
        Street = street;
        ZipCode = zipCode;
    }
}