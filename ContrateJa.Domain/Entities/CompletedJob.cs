namespace ContrateJa.Domain.Entities
{
  public class CompletedJob
  {
    public int Id { get; set; }
    public int JobId { get; set; }
    public int FreelancerId { get; set; }
    public int ContractorId { get; set; }
    public DateTime CompletedAt { get; set; }

    public CompletedJob(int jobId, int freelancerId, int contractorId)
    {
      JobId = jobId;
      FreelancerId = freelancerId;
      ContractorId = contractorId;
    }
  }
}
