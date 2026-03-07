using ContrateJa.Domain.Enums;
using ContrateJa.Domain.Primitives;
using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Domain.Entities;

public sealed class Job : Entity
{
    public long ContractorId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public State State { get; private set; }
    public City City { get; private set; }
    public Street Street { get; private set; }
    public ZipCode ZipCode { get; private set; }
    public EJobStatus Status { get; private set; }

    private Job() { }

    private Job(long contractorId, string title, string description, State state, City city, Street street, ZipCode zipCode)
    {
        ContractorId = contractorId;
        Title = title;
        Description = description;
        State = state;
        City = city;
        Street = street;
        ZipCode = zipCode;
        Status = EJobStatus.Open;
    }

    public static Job Create(long contractorId, string title, string description, State state, City city, Street street, ZipCode zipCode)
    {
        if (contractorId <= 0)
            throw new ArgumentOutOfRangeException(nameof(contractorId), "Contractor Id cannot be lower than 1.");

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be null or empty.", nameof(title));

        title = title.Trim();

        if (title.Length > 150)
            throw new ArgumentException("Title cannot exceed 150 characters.", nameof(title));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be null or empty.", nameof(description));

        description = description.Trim();

        if (description.Length > 1000)
            throw new ArgumentException("Description cannot exceed 1000 characters.", nameof(description));

        if (state is null)
            throw new ArgumentNullException(nameof(state));

        if (city is null)
            throw new ArgumentNullException(nameof(city));

        if (street is null)
            throw new ArgumentNullException(nameof(street));

        if (zipCode is null)
            throw new ArgumentNullException(nameof(zipCode));

        return new Job(contractorId, title, description, state, city, street, zipCode);
    }

    public void UpdateTitle(string newTitle)
    {
        if (string.IsNullOrWhiteSpace(newTitle))
            throw new ArgumentException("Title cannot be null or empty.", nameof(newTitle));

        newTitle = newTitle.Trim();

        if (newTitle.Length > 150)
            throw new ArgumentException("Title cannot exceed 150 characters.", nameof(newTitle));

        if (newTitle == Title)
            return;

        Title = newTitle;
        Touch();
    }

    public void UpdateDescription(string newDescription)
    {
        if (string.IsNullOrWhiteSpace(newDescription))
            throw new ArgumentException("Description cannot be null or empty.", nameof(newDescription));

        newDescription = newDescription.Trim();

        if (newDescription.Length > 1000)
            throw new ArgumentException("Description cannot exceed 1000 characters.", nameof(newDescription));

        if (newDescription == Description)
            return;

        Description = newDescription;
        Touch();
    }

    public void UpdateAddress(State newState, City newCity, Street newStreet, ZipCode newZipCode)
    {
        if (newState is null)
            throw new ArgumentNullException(nameof(newState));

        if (newCity is null)
            throw new ArgumentNullException(nameof(newCity));

        if (newStreet is null)
            throw new ArgumentNullException(nameof(newStreet));

        if (newZipCode is null)
            throw new ArgumentNullException(nameof(newZipCode));

        if (Equals(State, newState) &&
            Equals(City, newCity) &&
            Equals(Street, newStreet) &&
            Equals(ZipCode, newZipCode))
            return;

        State = newState;
        City = newCity;
        Street = newStreet;
        ZipCode = newZipCode;
        Touch();
    }

    public void UpdateStatus(EJobStatus newStatus)
    {
        if (!Enum.IsDefined(typeof(EJobStatus), newStatus))
            throw new ArgumentOutOfRangeException(nameof(newStatus), "Invalid job status.");

        if (newStatus == Status)
            return;

        if (!IsValidTransition(Status, newStatus))
            throw new InvalidOperationException($"Invalid transition: {Status} -> {newStatus}.");

        Status = newStatus;
        Touch();
    }

    private static bool IsValidTransition(EJobStatus current, EJobStatus next)
    {
        return current switch
        {
            EJobStatus.Open => next is EJobStatus.InProgress or EJobStatus.Closed,
            EJobStatus.InProgress => next is EJobStatus.Closed,
            EJobStatus.Closed => false,
            _ => false
        };
    }
}