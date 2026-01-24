using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Tests.Domain.ValueObjects
{
  public class StreetTests
  {
    [Fact]
    public void CreateStreet_ValidName_ShouldCreateStreet()
    {
      var street = new Street("Av Paulista");
      Assert.Equal("Av Paulista", street.Name);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void CreateStreet_NullOrEmpty_ShouldThrowArgumentException(string name)
    {
      var ex = Assert.Throws<ArgumentException>(() => new Street(name));
      Assert.Equal("Street is required.", ex.Message);
    }

    [Theory]
    [InlineData("Av")]
    [InlineData(
      "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA")]
    public void CreateStreet_InvalidLength_ShouldThrowArgumentException(string name)
    {
      var ex = Assert.Throws<ArgumentException>(() => new Street(name));
      Assert.Equal("Invalid street name.", ex.Message);
    }

    [Theory]
    [InlineData("Street@Name")]
    [InlineData("Street#1")]
    [InlineData("Street!")]
    public void CreateStreet_InvalidCharacters_ShouldThrowArgumentException(string name)
    {
      var ex = Assert.Throws<ArgumentException>(() => new Street(name));
      Assert.Equal("Invalid street name.", ex.Message);
    }

    [Fact]
    public void Streets_WithSameName_ShouldBeEqual()
    {
      var s1 = new Street("Rua das Flores");
      var s2 = new Street("Rua das Flores");

      Assert.Equal(s1, s2);
      Assert.Equal(s1.GetHashCode(), s2.GetHashCode());
    }

    [Fact]
    public void Streets_WithDifferentNames_ShouldNotBeEqual()
    {
      var s1 = new Street("Rua A");
      var s2 = new Street("Rua B");

      Assert.NotEqual(s1, s2);
    }
  }
}