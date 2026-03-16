using MediatR;

namespace ContrateJa.Application.UseCases.Jobs.CreateJob;

public sealed class CreateJobCommand : IRequest
{
    public long ContractorId { get; }
    public string Title { get; }
    public string Description { get; }
    public string State { get; }
    public string City { get; }
    public string Street { get; }
    public string ZipCode { get; }
    
    public CreateJobCommand(long contractorId, string title, string description, string state, string city, string street, string zipCode)
    {
        ContractorId = contractorId > 0 ? contractorId : throw new ArgumentOutOfRangeException(nameof(contractorId));
        Title = title;
        Description = description;
        State = state;
        City = city;
        Street = street;
        ZipCode = zipCode;
    }
}