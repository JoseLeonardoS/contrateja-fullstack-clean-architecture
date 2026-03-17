using ContrateJa.Domain.Enums;
using ContrateJa.Domain.Primitives;
using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Domain.Entities;

public sealed class Proposal : Entity
{
    public long JobId { get; private set; }
    public long FreelancerId { get; private set; }
    public Money Money { get; private set; }
    public string CoverLetter { get; private set; }
    public EProposalStatus Status { get; private set; }
    public DateTime SubmittedAt { get; private set; }

    private Proposal() { }

    private Proposal(long jobId, long freelancerId, Money money, string coverLetter)
    {
        JobId = jobId;
        FreelancerId = freelancerId;
        Money = money;
        CoverLetter = coverLetter;
        Status = EProposalStatus.Sent;
        SubmittedAt = DateTime.UtcNow;
    }

    public static Proposal Create(long jobId, long freelancerId, Money amount, string coverLetter)
    {
        if (jobId <= 0)
            throw new ArgumentOutOfRangeException(nameof(jobId), "Job Id cannot be lower than 1.");

        if (freelancerId <= 0)
            throw new ArgumentOutOfRangeException(nameof(freelancerId), "Freelancer Id cannot be lower than 1.");

        if (amount is null)
            throw new ArgumentNullException(nameof(amount));

        coverLetter = NormalizeAndValidateCoverLetter(coverLetter);

        return new Proposal(jobId, freelancerId, amount, coverLetter);
    }

    public void EditProposal(Money amount, string coverLetter)
    {
        if (Status != EProposalStatus.Sent)
            throw new InvalidOperationException("Proposal can only be edited when status is Sent.");

        if (amount is null)
            throw new ArgumentNullException(nameof(amount));

        coverLetter = NormalizeAndValidateCoverLetter(coverLetter);

        if (Equals(Money, amount) && CoverLetter == coverLetter)
            return;

        Money = amount;
        CoverLetter = coverLetter;
        Touch();
    }

    public void EditAmount(Money amount)
    {
        if (Status != EProposalStatus.Sent)
            throw new InvalidOperationException("Amount can only be changed when status is Sent.");

        if (amount is null)
            throw new ArgumentNullException(nameof(amount));

        if (Equals(Money, amount))
            return;

        Money = amount;
        Touch();
    }

    public void EditCoverLetter(string coverLetter)
    {
        if (Status != EProposalStatus.Sent)
            throw new InvalidOperationException("CoverLetter can only be changed when status is Sent.");

        coverLetter = NormalizeAndValidateCoverLetter(coverLetter);

        if (CoverLetter == coverLetter)
            return;

        CoverLetter = coverLetter;
        Touch();
    }

    public void EditStatus(EProposalStatus newStatus)
    {
        if (!Enum.IsDefined(typeof(EProposalStatus), newStatus))
            throw new ArgumentOutOfRangeException(nameof(newStatus), "Invalid proposal status.");

        if (newStatus == Status)
            return;

        if (!IsValidTransition(Status, newStatus))
            throw new InvalidOperationException($"Invalid transition: {Status} -> {newStatus}.");

        Status = newStatus;
        Touch();
    }

    private static bool IsValidTransition(EProposalStatus current, EProposalStatus next)
    {
        return current switch
        {
            EProposalStatus.Sent => next is EProposalStatus.Accepted or EProposalStatus.Rejected,
            EProposalStatus.Accepted => false,
            EProposalStatus.Rejected => false,
            _ => false
        };
    }

    private static string NormalizeAndValidateCoverLetter(string coverLetter)
    {
        if (string.IsNullOrWhiteSpace(coverLetter))
            throw new ArgumentException("CoverLetter cannot be null or empty.", nameof(coverLetter));

        coverLetter = coverLetter.Trim();

        if (coverLetter.Length > 1000)
            throw new ArgumentException("CoverLetter must not exceed 1000 characters.", nameof(coverLetter));

        return coverLetter;
    }
}