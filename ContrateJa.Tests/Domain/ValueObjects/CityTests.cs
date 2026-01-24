using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Tests.Domain.ValueObjects
{
  public class CityTests
  {
    [Fact]
    public void CreateCity_ValidName_ShouldCreateCity()
    {
      var city = new City("São Paulo");
      Assert.Equal("São Paulo", city.Name);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void CreateCity_NullOrEmptyName_ShouldThrowArgumentException(string name)
    {
      var exception = Assert.Throws<ArgumentException>(() => new City(name));
      Assert.Equal("City is required.", exception.Message);
    }

    [Theory]
    [InlineData("A")]
    [InlineData("ThisCityNameIsWayTooLongToBeConsideredValidBecauseItExceedsTheMaximumAllowedLengthOfOneHundredCharactersWhichIsNotAcceptable")]
    public void CreateCity_InvalidLengthName_ShouldThrowArgumentException(string name)
    {
      var exception = Assert.Throws<ArgumentException>(() => new City(name));
      Assert.Equal("Invalid city name.", exception.Message);
    }

    [Theory]
    [InlineData("City123")]
    [InlineData("City@Name")]
    [InlineData("City!Name")]
    public void CreateCity_InvalidCharactersInName_ShouldThrowArgumentException(string name)
    {
      var exception = Assert.Throws<ArgumentException>(() => new City(name));
      Assert.Equal("Invalid city name.", exception.Message);
    }

    [Fact]
    public void Cities_WithSameName_ShouldBeEqual()
    {
      var city1 = new City("Rio de Janeiro");
      var city2 = new City("Rio de Janeiro");

      Assert.Equal(city1, city2);
      Assert.True(city1.Equals(city2));
      Assert.Equal(city1.GetHashCode(), city2.GetHashCode());
    }

    [Fact]
    public void Cities_WithDifferentNames_ShouldNotBeEqual()
    {
      var city1 = new City("Rio de Janeiro");
      var city2 = new City("São Paulo");

      Assert.NotEqual(city1, city2);
      Assert.False(city1.Equals(city2));
    }
  }
}