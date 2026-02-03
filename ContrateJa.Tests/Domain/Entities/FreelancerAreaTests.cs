using ContrateJa.Domain.Entities;
using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Tests.Domain.Entities
{
    public sealed class FreelancerAreaTests
    {
        public static IEnumerable<object[]> ValidFreelancerIds()
        {
            yield return new object[] { 1L };
            yield return new object[] { 2L };
            yield return new object[] { 999L };
            yield return new object[] { long.MaxValue };
        }

        public static IEnumerable<object[]> InvalidFreelancerIds()
        {
            yield return new object[] { 0L };
            yield return new object[] { -1L };
            yield return new object[] { long.MinValue };
        }

        [Theory]
        [MemberData(nameof(InvalidFreelancerIds))]
        public void Create_WithInvalidFreelancerId_ThrowsArgumentOutOfRangeException(long freelancerId)
        {
            var area = new Area(new  State("CE"), new City("Fortaleza"));

            Assert.Throws<ArgumentOutOfRangeException>(() => FreelancerArea.Create(freelancerId, area));
        }

        [Fact]
        public void Create_WithNullArea_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => FreelancerArea.Create(1L, null));
        }

        [Theory]
        [MemberData(nameof(ValidFreelancerIds))]
        public void Create_WithValidInput_CreatesEntity(long freelancerId)
        {
            var area = new Area( new State("CE"), new City("Fortaleza"));

            var entity = FreelancerArea.Create(freelancerId, area);

            Assert.NotNull(entity);
            Assert.Equal(freelancerId, entity.FreelancerId);
            Assert.Same(area, entity.Area);
        }
    }
}