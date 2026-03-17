using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Enums;
using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Tests.Domain.Entities;

public sealed class ProposalTests
{
    private static Proposal CreateProposal(
        long jobId = 1,
        long freelancerId = 2,
        Money? amount = null,
        string coverLetter = "Carta de apresentação de exemplo.")
    {
        amount ??= new Money(100, "BRL");
        return Proposal.Create(jobId, freelancerId, amount, coverLetter);
    }

    [Fact]
    public void Create_SetsTimestamps()
    {
        var before = DateTime.UtcNow;
        var proposal = CreateProposal();
        var after = DateTime.UtcNow;

        Assert.InRange(proposal.CreatedAt, before, after);
        Assert.InRange(proposal.UpdatedAt, before, after);
        Assert.InRange(proposal.SubmittedAt, before, after);
        Assert.True(proposal.UpdatedAt >= proposal.CreatedAt);
    }

    [Fact]
    public void Create_SetsStatusToSent()
    {
        var proposal = CreateProposal();
        Assert.Equal(EProposalStatus.Sent, proposal.Status);
    }

    [Fact]
    public void Create_WithInvalidJobId_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => CreateProposal(jobId: 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => CreateProposal(jobId: -1));
    }

    [Fact]
    public void Create_WithInvalidFreelancerId_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => CreateProposal(freelancerId: 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => CreateProposal(freelancerId: -1));
    }

    [Fact]
    public void Create_WithNullAmount_Throws()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => 
            Proposal.Create(1, 2, null!, "Carta de apresentação de exemplo."));
        Assert.Equal("amount", ex.ParamName);
    }

    public static IEnumerable<object[]> NullOrWhitespaceCoverLetters()
    {
        yield return new object[] { null! };
        yield return new object[] { "" };
        yield return new object[] { "   " };
    }

    [Theory]
    [MemberData(nameof(NullOrWhitespaceCoverLetters))]
    public void Create_WithNullOrWhitespaceCoverLetter_Throws(string? coverLetter)
    {
        Assert.Throws<ArgumentException>(() => CreateProposal(coverLetter: coverLetter!));
    }

    [Fact]
    public void Create_WithCoverLetterExceeding1000Chars_Throws()
    {
        Assert.Throws<ArgumentException>(() => CreateProposal(coverLetter: new string('a', 1001)));
    }

    [Fact]
    public void EditProposal_WithNullAmount_Throws()
    {
        var proposal = CreateProposal();
        Assert.Throws<ArgumentNullException>(() => proposal.EditProposal(null!, "Nova carta."));
    }

    [Theory]
    [MemberData(nameof(NullOrWhitespaceCoverLetters))]
    public void EditProposal_WithNullOrWhitespaceCoverLetter_Throws(string? coverLetter)
    {
        var proposal = CreateProposal();
        Assert.Throws<ArgumentException>(() => proposal.EditProposal(new Money(200, "BRL"), coverLetter!));
    }

    [Fact]
    public void EditProposal_WhenDifferent_UpdatesAndUpdatedAt()
    {
        var proposal = CreateProposal();
        var oldUpdatedAt = proposal.UpdatedAt;

        var newAmount = new Money(200, "BRL");
        proposal.EditProposal(newAmount, "Nova carta de apresentação.");

        Assert.Equal(newAmount, proposal.Money);
        Assert.Equal("Nova carta de apresentação.", proposal.CoverLetter);
        Assert.True(proposal.UpdatedAt > oldUpdatedAt);
    }

    [Fact]
    public void EditProposal_WhenSame_DoesNotUpdateUpdatedAt()
    {
        var proposal = CreateProposal();
        var oldUpdatedAt = proposal.UpdatedAt;

        proposal.EditProposal(proposal.Money, proposal.CoverLetter);

        Assert.Equal(oldUpdatedAt, proposal.UpdatedAt);
    }

    public static IEnumerable<object[]> StatusesThatBlockEditing()
    {
        yield return new object[] { EProposalStatus.Accepted };
        yield return new object[] { EProposalStatus.Rejected };
    }

    [Theory]
    [MemberData(nameof(StatusesThatBlockEditing))]
    public void EditProposal_WhenStatusIsNotSent_Throws(EProposalStatus status)
    {
        var proposal = CreateProposal();
        proposal.EditStatus(status);

        Assert.Throws<InvalidOperationException>(() => proposal.EditProposal(new Money(200, "BRL"), "Nova carta."));
    }

    [Theory]
    [MemberData(nameof(StatusesThatBlockEditing))]
    public void EditAmount_WhenStatusIsNotSent_Throws(EProposalStatus status)
    {
        var proposal = CreateProposal();
        proposal.EditStatus(status);

        Assert.Throws<InvalidOperationException>(() => proposal.EditAmount(new Money(200, "BRL")));
    }

    [Fact]
    public void EditAmount_WhenDifferent_UpdatesAmountAndUpdatedAt()
    {
        var proposal = CreateProposal();
        var oldUpdatedAt = proposal.UpdatedAt;

        var newAmount = new Money(500, "BRL");
        proposal.EditAmount(newAmount);

        Assert.Equal(newAmount, proposal.Money);
        Assert.True(proposal.UpdatedAt > oldUpdatedAt);
    }

    [Fact]
    public void EditAmount_WhenSame_DoesNotUpdateUpdatedAt()
    {
        var proposal = CreateProposal();
        var oldUpdatedAt = proposal.UpdatedAt;

        proposal.EditAmount(proposal.Money);

        Assert.Equal(oldUpdatedAt, proposal.UpdatedAt);
    }

    [Theory]
    [MemberData(nameof(StatusesThatBlockEditing))]
    public void EditCoverLetter_WhenStatusIsNotSent_Throws(EProposalStatus status)
    {
        var proposal = CreateProposal();
        proposal.EditStatus(status);

        Assert.Throws<InvalidOperationException>(() => proposal.EditCoverLetter("Nova carta."));
    }

    [Fact]
    public void EditCoverLetter_WhenDifferent_UpdatesCoverLetterAndUpdatedAt()
    {
        var proposal = CreateProposal();
        var oldUpdatedAt = proposal.UpdatedAt;

        proposal.EditCoverLetter("Nova carta de apresentação.");

        Assert.Equal("Nova carta de apresentação.", proposal.CoverLetter);
        Assert.True(proposal.UpdatedAt > oldUpdatedAt);
    }

    [Fact]
    public void EditCoverLetter_WhenSame_DoesNotUpdateUpdatedAt()
    {
        var proposal = CreateProposal();
        var oldUpdatedAt = proposal.UpdatedAt;

        proposal.EditCoverLetter(proposal.CoverLetter);

        Assert.Equal(oldUpdatedAt, proposal.UpdatedAt);
    }

    public static IEnumerable<object[]> ValidProposalStatusTransitions()
    {
        yield return new object[] { EProposalStatus.Sent, EProposalStatus.Accepted };
        yield return new object[] { EProposalStatus.Sent, EProposalStatus.Rejected };
    }

    [Theory]
    [MemberData(nameof(ValidProposalStatusTransitions))]
    public void EditStatus_WithValidTransition_UpdatesStatusAndUpdatedAt(EProposalStatus from, EProposalStatus to)
    {
        var proposal = CreateProposal();
        var oldUpdatedAt = proposal.UpdatedAt;

        proposal.EditStatus(to);

        Assert.Equal(to, proposal.Status);
        Assert.True(proposal.UpdatedAt > oldUpdatedAt);
    }

    public static IEnumerable<object[]> InvalidProposalStatusTransitions()
    {
        yield return new object[] { EProposalStatus.Accepted, EProposalStatus.Sent };
        yield return new object[] { EProposalStatus.Accepted, EProposalStatus.Rejected };
        yield return new object[] { EProposalStatus.Rejected, EProposalStatus.Sent };
        yield return new object[] { EProposalStatus.Rejected, EProposalStatus.Accepted };
    }

    [Theory]
    [MemberData(nameof(InvalidProposalStatusTransitions))]
    public void EditStatus_WithInvalidTransition_Throws(EProposalStatus from, EProposalStatus to)
    {
        var proposal = CreateProposal();
        proposal.EditStatus(from);

        Assert.Throws<InvalidOperationException>(() => proposal.EditStatus(to));
    }

    [Fact]
    public void EditStatus_WhenSame_DoesNotUpdateUpdatedAt()
    {
        var proposal = CreateProposal();
        var oldUpdatedAt = proposal.UpdatedAt;

        proposal.EditStatus(proposal.Status);

        Assert.Equal(oldUpdatedAt, proposal.UpdatedAt);
    }
}