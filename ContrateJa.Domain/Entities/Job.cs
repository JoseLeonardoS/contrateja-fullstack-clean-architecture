namespace ContrateJa.Domain.Entities
{
  public class Job
  {
    public int Id { get; private set; }
    public int ContractorId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string State { get; private set; }
    public string City { get; private set; }
    public string Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Job() { }

    private Job(int contractorId, string title, string description, string state, string city, string status)
    {
      ContractorId = contractorId;
      Title = title;
      Description = description;
      State = state;
      City = city;
      Status = status;
      CreatedAt = DateTime.UtcNow;
    }

    public static Job Create(int contractorId, string title, string description, string state, string city, string status)
    {
      // TODO: Additional validation logic can be added here
      return new Job(contractorId, title, description, state, city, status);
    }
  }
}
