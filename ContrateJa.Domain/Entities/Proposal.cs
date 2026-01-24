namespace ContrateJa.Domain.Entities
{
  public class Proposal
  {
    public int Id { get; set; }
    public int JobId { get; set; }
    public int FreelancerId { get; set; }
    public decimal Amount { get; set; }
    public string CoverLetter { get; set; }
    public string Status { get; set; }
    public DateTime SubmittedAt { get; set; }

    public Proposal(int jobId, int freelancerId, decimal amount, string coverLetter, string status)
    {
      JobId = jobId;
      FreelancerId = freelancerId;
      Amount = amount;
      CoverLetter = coverLetter;
      Status = status;
    }
  }
}