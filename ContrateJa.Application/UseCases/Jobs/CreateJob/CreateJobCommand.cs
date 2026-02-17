using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Application.UseCases.Jobs.CreateJob;

public sealed class CreateJobCommand
{
    public long ContractorId { get; }
    public string Title { get; }
    public string Description { get; }
    public State State { get; }
    public City City { get; }
    public Street Street { get; }
    public ZipCode ZipCode { get; }
    
    public CreateJobCommand(long contractorId, string title, string description, State state, City city, Street street, ZipCode zipCode)
    {
        ContractorId = contractorId;
        Title = title;
        Description = description;
        State = state;
        City = city;
        Street = street;
        ZipCode = zipCode;
    }
}