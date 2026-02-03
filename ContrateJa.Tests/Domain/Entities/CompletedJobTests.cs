using ContrateJa.Domain.Entities;

namespace ContrateJa.Tests.Domain.Entities
{
    public sealed class CompletedJobTests
    {
        public static IEnumerable<object[]> InvalidIds()
        {
            yield return new object[] { 0L };
            yield return new object[] { -1L };
            yield return new object[] { -999L };
        }

        [Theory]
        [MemberData(nameof(InvalidIds))]
        public void Create_WithInvalidJobId_ThrowsArgumentOutOfRangeException(long jobId)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => CompletedJob.Create(jobId, 1L, 2L));
        }

        [Theory]
        [MemberData(nameof(InvalidIds))]
        public void Create_WithInvalidFreelancerId_ThrowsArgumentOutOfRangeException(long freelancerId)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => CompletedJob.Create(1L, freelancerId, 2L));
        }

        [Theory]
        [MemberData(nameof(InvalidIds))]
        public void Create_WithInvalidContractorId_ThrowsArgumentOutOfRangeException(long contractorId)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => CompletedJob.Create(1L, 2L, contractorId));
        }

        [Fact]
        public void Create_WithSameFreelancerAndContractor_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => CompletedJob.Create(1L, 10L, 10L));
        }

        [Fact]
        public void Create_WithValidData_SetsProperties()
        {
            var before = DateTime.UtcNow;

            var entity = CompletedJob.Create(5L, 10L, 20L);

            var after = DateTime.UtcNow;

            Assert.NotNull(entity);
            Assert.Equal(5L, entity.JobId);
            Assert.Equal(10L, entity.FreelancerId);
            Assert.Equal(20L, entity.ContractorId);
            Assert.True(entity.CompletedAt >= before && entity.CompletedAt <= after);
        }
    }
}