using ContrateJa.Domain.Entities;
using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Tests.Domain.Entities;

public sealed class FreelancerAreaTests
{
    private static FreelancerArea CreateFreelancerArea(
        long freelancerId = 1,
        Area? area = null)
    {
        area ??= new Area(new State("SP"), new City("São Paulo"));
        return FreelancerArea.Create(freelancerId, area);
    }

    [Fact]
    public void Create_SetsTimestamps()
    {
        var before = DateTime.UtcNow;
        var freelancerArea = CreateFreelancerArea();
        var after = DateTime.UtcNow;

        Assert.InRange(freelancerArea.CreatedAt, before, after);
        Assert.InRange(freelancerArea.UpdatedAt, before, after);
        Assert.True(freelancerArea.UpdatedAt >= freelancerArea.CreatedAt);
    }

    [Fact]
    public void Create_WithInvalidFreelancerId_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => CreateFreelancerArea(freelancerId: 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => CreateFreelancerArea(freelancerId: -1));
    }

    [Fact]
    public void Create_WithNullArea_Throws()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => FreelancerArea.Create(1, null!));
        Assert.Equal("area", ex.ParamName);
    }

    [Fact]
    public void Create_WithValidArgs_SetsProperties()
    {
        var area = new Area(new State("SP"), new City("São Paulo"));
        var freelancerArea = CreateFreelancerArea(freelancerId: 1, area: area);

        Assert.Equal(1, freelancerArea.FreelancerId);
        Assert.Equal(area, freelancerArea.Area);
    }

    [Fact]
    public void ChangeArea_WithNull_Throws()
    {
        var freelancerArea = CreateFreelancerArea();
        Assert.Throws<ArgumentNullException>(() => freelancerArea.ChangeArea(null!));
    }

    [Fact]
    public void ChangeArea_WhenDifferent_UpdatesAreaAndUpdatedAt()
    {
        var freelancerArea = CreateFreelancerArea();
        var oldUpdatedAt = freelancerArea.UpdatedAt;

        var newArea = new Area(new State("MG"), new City("Belo Horizonte"));
        freelancerArea.ChangeArea(newArea);

        Assert.Equal(newArea, freelancerArea.Area);
        Assert.True(freelancerArea.UpdatedAt > oldUpdatedAt);
    }

    [Fact]
    public void ChangeArea_WhenSame_DoesNotUpdateUpdatedAt()
    {
        var freelancerArea = CreateFreelancerArea();
        var oldUpdatedAt = freelancerArea.UpdatedAt;

        freelancerArea.ChangeArea(freelancerArea.Area);

        Assert.Equal(oldUpdatedAt, freelancerArea.UpdatedAt);
    }
}