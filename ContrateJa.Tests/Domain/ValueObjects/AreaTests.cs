using ContrateJa.Domain.ValueObjects;
using Xunit;

namespace ContrateJa.Tests.Domain.ValueObjects
{
  public class AreaTests
  {
    [Fact]
    public void Area_WithValidStateAndCity_ShouldCreateSuccessfully()
    {
      var state = new State("SP");
      var city = new City("São Paulo");

      var area = new Area(state, city);

      Assert.Equal(state, area.State);
      Assert.Equal(city, area.City);
    }

    [Fact]
    public void Area_WithNullState_ShouldThrowArgumentNullException()
    {
      var city = new City("São Paulo");

      var ex = Assert.Throws<ArgumentNullException>(() =>
        new Area(null!, city)
      );

      Assert.Equal("state", ex.ParamName);
    }

    [Fact]
    public void Area_WithNullCity_ShouldThrowArgumentNullException()
    {
      var state = new State("SP");

      var ex = Assert.Throws<ArgumentNullException>(() =>
        new Area(state, null!)
      );

      Assert.Equal("city", ex.ParamName);
    }

    [Fact]
    public void Area_WithSameValues_ShouldBeEqual()
    {
      var area1 = new Area(new State("SP"), new City("São Paulo"));
      var area2 = new Area(new State("SP"), new City("São Paulo"));

      Assert.Equal(area1, area2);
    }

    [Fact]
    public void Area_WithDifferentCity_ShouldNotBeEqual()
    {
      var area1 = new Area(new State("SP"), new City("São Paulo"));
      var area2 = new Area(new State("SP"), new City("Campinas"));

      Assert.NotEqual(area1, area2);
    }

    [Fact]
    public void Area_WithDifferentState_ShouldNotBeEqual()
    {
      var area1 = new Area(new State("SP"), new City("São Paulo"));
      var area2 = new Area(new State("RJ"), new City("São Paulo"));

      Assert.NotEqual(area1, area2);
    }

    [Fact]
    public void Area_ToString_ShouldReturnCityCommaState()
    {
      var area = new Area(new State("SP"), new City("São Paulo"));

      Assert.Equal("São Paulo, SP", area.ToString());
    }

    [Fact]
    public void Area_GetHashCode_SameValues_ShouldBeEqual()
    {
      var area1 = new Area(new State("SP"), new City("São Paulo"));
      var area2 = new Area(new State("SP"), new City("São Paulo"));

      Assert.Equal(area1.GetHashCode(), area2.GetHashCode());
    }
  }
}
