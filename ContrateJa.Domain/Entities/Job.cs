using ContrateJa.Domain.Enums;
using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Domain.Entities
{
  public sealed class Job
  {
    public long Id { get; private set; }
    public long ContractorId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public State State { get; private set; }
    public City City { get; private set; }
    public Street Street { get; private set; }
    public ZipCode ZipCode { get; private set; }
    public EJobStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Job() { }

    private Job(long contractorId, string title, string description, State state, City city, Street street, ZipCode zipCode, EJobStatus status)
    {
      ContractorId = contractorId;
      Title = title;
      Description = description;
      State = state;
      City = city;
      Street = street;
      ZipCode = zipCode;
      Status = status;
      CreatedAt = DateTime.UtcNow;
      UpdatedAt = DateTime.UtcNow;
    }

    public static Job Create(long contractorId, string title, string description, State state, City city, Street street, ZipCode zipCode)
    {
      if (contractorId <= 0)
        throw new ArgumentException("ContractorId must be positive.", nameof(contractorId));

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

      if (state == null)
        throw new ArgumentNullException(nameof(state));

      if (city == null)
        throw new ArgumentNullException(nameof(city));

      if (street == null)
        throw new ArgumentNullException(nameof(street));

      if (zipCode == null)
        throw new ArgumentNullException(nameof(zipCode));

      return new Job(contractorId, title, description, state, city, street, zipCode, EJobStatus.Open);
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

    public void UpdateState(State newState)
    {
      if (newState == null)
        throw new ArgumentNullException(nameof(newState));

      if (Equals(State, newState))
        return;

      State = newState;
      Touch();
    }

    public void UpdateCity(City newCity)
    {
      if (newCity == null)
        throw new ArgumentNullException(nameof(newCity));

      if (Equals(City, newCity))
        return;

      City = newCity;
      Touch();
    }

    public void UpdateStreet(Street newStreet)
    {
      if (newStreet == null)
        throw new ArgumentNullException(nameof(newStreet));

      if (Equals(Street, newStreet))
        return;

      Street = newStreet;
      Touch();
    }

    public void UpdateZipCode(ZipCode newZipCode)
    {
      if (newZipCode == null)
        throw new ArgumentNullException(nameof(newZipCode));

      if (Equals(ZipCode, newZipCode))
        return;

      ZipCode = newZipCode;
      Touch();
    }

    public void UpdateStatus(EJobStatus newStatus)
    {
      if (!Enum.IsDefined(typeof(EJobStatus), newStatus))
        throw new ArgumentOutOfRangeException(nameof(newStatus), "Invalid job status.");

      if (Status == EJobStatus.Closed)
        throw new InvalidOperationException("You can't change the job status.");

      if (newStatus == Status)
        return;

      Status = newStatus;
      Touch();
    }

    private void Touch()
    {
      UpdatedAt = DateTime.UtcNow;
    }
  }
}
