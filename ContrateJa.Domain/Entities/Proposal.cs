namespace ContrateJa.Domain.Entities
{
  public sealed class Proposal
  {
    public long Id { get; private set; }
    public long JobId { get; private set; }
    public long FreelancerId { get; private set; }
    public decimal Amount { get; private set; }
    public string CoverLetter { get; private set; }
    public string Status { get; private set; }
    public DateTime SubmittedAt { get; private set; }

    private Proposal() { }

    private Proposal(int jobId, int freelancerId, decimal amount, string coverLetter, string status)
    {
      JobId = jobId;
      FreelancerId = freelancerId;
      Amount = amount;
      CoverLetter = coverLetter;
      Status = status;
      SubmittedAt = DateTime.UtcNow;
    }

    public static void CreateProposal(long jobId, long freelancerId, decimal amount, string coverLetter, string satatus)
    {
      // TODO: Refinar código de proposal e  criar VOs necessários.
    }
  }
}