using ContrateJa.Domain.Entities;

namespace ContrateJa.Tests.Domain.Entities;

public sealed class CompletedJobTests
{
    private static CompletedJob CreateCompletedJob(
        long jobId = 1,
        long freelancerId = 2,
        long contractorId = 3)
    {
        return CompletedJob.Create(jobId, freelancerId, contractorId);
    }

    [Fact]
    public void Create_SetsTimestamps()
    {
        var before = DateTime.UtcNow;
        var completedJob = CreateCompletedJob();
        var after = DateTime.UtcNow;

        Assert.InRange(completedJob.CreatedAt, before, after);
        Assert.InRange(completedJob.CompletedAt, before, after);
    }

    [Fact]
    public void Create_WithInvalidJobId_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => CreateCompletedJob(jobId: 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => CreateCompletedJob(jobId: -1));
    }

    [Fact]
    public void Create_WithInvalidFreelancerId_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => CreateCompletedJob(freelancerId: 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => CreateCompletedJob(freelancerId: -1));
    }

    [Fact]
    public void Create_WithInvalidContractorId_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => CreateCompletedJob(contractorId: 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => CreateCompletedJob(contractorId: -1));
    }

    [Fact]
    public void Create_WhenFreelancerEqualsContractor_Throws()
    {
        Assert.Throws<ArgumentException>(() => CreateCompletedJob(freelancerId: 1, contractorId: 1));
    }

    [Fact]
    public void Create_WithValidArgs_SetsProperties()
    {
        var completedJob = CreateCompletedJob(jobId: 1, freelancerId: 2, contractorId: 3);

        Assert.Equal(1, completedJob.JobId);
        Assert.Equal(2, completedJob.FreelancerId);
        Assert.Equal(3, completedJob.ContractorId);
    }
}