using ContrateJa.Domain.Enums;
using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Domain.Entities
{
  public sealed class Proposal
  {
    public long Id { get; private set; }
    public long JobId { get; private set; }
    public long FreelancerId { get; private set; }
    public Money Amount { get; private set; }
    public string CoverLetter { get; private set; }
    public EProposalStatus Status { get; private set; }
    public DateTime SubmittedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Proposal() { }

    private Proposal(long jobId, long freelancerId, Money amount, string coverLetter)
    {
      JobId = jobId;
      FreelancerId = freelancerId;
      Amount = amount;
      CoverLetter = coverLetter;
      Status = EProposalStatus.Sent;
      SubmittedAt = DateTime.UtcNow;
      UpdatedAt = DateTime.UtcNow;
    }

    public static Proposal Create(long jobId, long freelancerId, Money amount, string coverLetter)
    {
      if (jobId <= 0)
        throw new ArgumentException("Job Id must be positive.", nameof(jobId));

      if (freelancerId <= 0)
        throw new ArgumentException("Freelancer Id must be positive.", nameof(freelancerId));
      
      if(amount == null)
        throw new ArgumentNullException(nameof(amount));
      
      if(string.IsNullOrWhiteSpace(coverLetter))
        throw new ArgumentException("Cover letter cannot be null or white space.", nameof(coverLetter));
      
      coverLetter = coverLetter.Trim();
      
      if(coverLetter.Length > 1000)
        throw new ArgumentException("Cover letter cannot be longer than 1000 characters.", nameof(coverLetter));

      return new Proposal(jobId, freelancerId, amount, coverLetter);
    }

    public void EditProposal(Money amount, string coverLetter)
    {
      // TODO: lógica para editar proposta 
    }

    public void EditAmount(Money amount)
    {
      // TODO: lógica para editar amount
    }

    public void EditCoverLetter(string coverLetter)
    {
      // TODO: lógica para editar Cover Letter
    }

    public void EditStatus(EProposalStatus status)
    {
      // TODO: lógica para editar Status
    }

    private void Touch()
      => UpdatedAt = DateTime.UtcNow;
  }
}
