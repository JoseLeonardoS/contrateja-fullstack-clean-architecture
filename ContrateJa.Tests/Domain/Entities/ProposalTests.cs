using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Enums;
using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Tests.Domain.Entities
{
  public sealed class ProposalTests
  {
    private static Money CreateValidMoney()
    {
      return new Money(100m);
    }

    public static IEnumerable<object[]> InvalidIds()
    {
      yield return new object[] { 0L };
      yield return new object[] { -1L };
    }

    public static IEnumerable<object[]> InvalidCoverLetters()
    {
      yield return new object[] { "" };
      yield return new object[] { " " };
      yield return new object[] { "   " };
      yield return new object[] { "\t" };
      yield return new object[] { "\n" };
    }

    [Fact]
    public void Create_WhenValid_SetsDefaultValues()
    {
      var proposal = Proposal.Create(1, 2, CreateValidMoney(), "Hello");

      Assert.Equal(1, proposal.JobId);
      Assert.Equal(2, proposal.FreelancerId);
      Assert.Equal("Hello", proposal.CoverLetter);
      Assert.Equal(EProposalStatus.Sent, proposal.Status);
      Assert.NotEqual(default, proposal.SubmittedAt);
      Assert.NotEqual(default, proposal.UpdatedAt);
    }

    [Fact]
    public void Create_TrimsCoverLetter()
    {
      var proposal = Proposal.Create(1, 2, CreateValidMoney(), "  Hello  ");

      Assert.Equal("Hello", proposal.CoverLetter);
    }

    [Theory]
    [MemberData(nameof(InvalidIds))]
    public void Create_WithInvalidJobId_Throws(long jobId)
    {
      Assert.Throws<ArgumentException>(() =>
        Proposal.Create(jobId, 1, CreateValidMoney(), "Hello"));
    }

    [Theory]
    [MemberData(nameof(InvalidIds))]
    public void Create_WithInvalidFreelancerId_Throws(long freelancerId)
    {
      Assert.Throws<ArgumentException>(() =>
        Proposal.Create(1, freelancerId, CreateValidMoney(), "Hello"));
    }

    [Fact]
    public void Create_WithNullAmount_Throws()
    {
      Assert.Throws<ArgumentNullException>(() =>
        Proposal.Create(1, 2, null!, "Hello"));
    }

    [Theory]
    [MemberData(nameof(InvalidCoverLetters))]
    public void Create_WithInvalidCoverLetter_Throws(string coverLetter)
    {
      Assert.Throws<ArgumentException>(() =>
        Proposal.Create(1, 2, CreateValidMoney(), coverLetter));
    }

    [Fact]
    public void EditProposal_WhenStatusNotSent_Throws()
    {
      var proposal = Proposal.Create(1, 2, CreateValidMoney(), "Hello");
      proposal.EditStatus(EProposalStatus.Accepted);

      Assert.Throws<InvalidOperationException>(() =>
        proposal.EditProposal(CreateValidMoney(), "New"));
    }

    [Fact]
    public void EditProposal_WhenValid_UpdatesFields()
    {
      var proposal = Proposal.Create(1, 2, CreateValidMoney(), "Hello");
      var before = proposal.UpdatedAt;

      Thread.Sleep(5);

      proposal.EditProposal(CreateValidMoney(), "  New  ");

      Assert.Equal("New", proposal.CoverLetter);
      Assert.True(proposal.UpdatedAt > before);
    }

    [Fact]
    public void EditAmount_WhenStatusNotSent_Throws()
    {
      var proposal = Proposal.Create(1, 2, CreateValidMoney(), "Hello");
      proposal.EditStatus(EProposalStatus.Rejected);

      Assert.Throws<InvalidOperationException>(() =>
        proposal.EditAmount(CreateValidMoney()));
    }

    [Fact]
    public void EditAmount_WhenValid_UpdatesAmount()
    {
      var proposal = Proposal.Create(1, 2, CreateValidMoney(), "Hello");
      var before = proposal.UpdatedAt;

      Thread.Sleep(100);

      proposal.EditAmount(new Money(101));

      Assert.True(proposal.UpdatedAt > before);
    }

    [Fact]
    public void EditCoverLetter_WhenStatusNotSent_Throws()
    {
      var proposal = Proposal.Create(1, 2, CreateValidMoney(), "Hello");
      proposal.EditStatus(EProposalStatus.Accepted);

      Assert.Throws<InvalidOperationException>(() =>
        proposal.EditCoverLetter("New"));
    }

    [Fact]
    public void EditCoverLetter_WhenValid_TrimsAndUpdates()
    {
      var proposal = Proposal.Create(1, 2, CreateValidMoney(), "Hello");
      var before = proposal.UpdatedAt;

      Thread.Sleep(5);

      proposal.EditCoverLetter("  New  ");

      Assert.Equal("New", proposal.CoverLetter);
      Assert.True(proposal.UpdatedAt > before);
    }

    [Fact]
    public void EditStatus_FromSentToAccepted_ChangesStatus()
    {
      var proposal = Proposal.Create(1, 2, CreateValidMoney(), "Hello");
      var before = proposal.UpdatedAt;

      Thread.Sleep(5);

      proposal.EditStatus(EProposalStatus.Accepted);

      Assert.Equal(EProposalStatus.Accepted, proposal.Status);
      Assert.True(proposal.UpdatedAt > before);
    }

    [Fact]
    public void EditStatus_InvalidTransition_Throws()
    {
      var proposal = Proposal.Create(1, 2, CreateValidMoney(), "Hello");
      proposal.EditStatus(EProposalStatus.Accepted);

      Assert.Throws<InvalidOperationException>(() =>
        proposal.EditStatus(EProposalStatus.Rejected));
    }

    [Fact]
    public void EditStatus_InvalidEnum_Throws()
    {
      var proposal = Proposal.Create(1, 2, CreateValidMoney(), "Hello");

      var invalid = (EProposalStatus)999;

      Assert.Throws<ArgumentOutOfRangeException>(() =>
        proposal.EditStatus(invalid));
    }
  }
}
