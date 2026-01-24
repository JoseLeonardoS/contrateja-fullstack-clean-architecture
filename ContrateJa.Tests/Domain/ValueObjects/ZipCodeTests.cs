using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Tests.Domain.ValueObjects
{
  public class ZipCodeTests
  {
    [Fact]
    public void CreateZipCode_ValidNumericValue_ShouldCreateZipCode()
    {
      var zipCode = new ZipCode("12345678");
      Assert.Equal("12345678", zipCode.Value);
    }

    [Fact]
    public void CreateZipCode_ValidFormattedValue_ShouldNormalizeAndCreateZipCode()
    {
      var zipCode = new ZipCode("12345-678");
      Assert.Equal("12345678", zipCode.Value);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CreateZipCode_NullOrEmpty_ShouldThrowArgumentException(string value)
    {
      var ex = Assert.Throws<ArgumentException>(() => new ZipCode(value));
      Assert.Equal("ZipCode is required.", ex.Message);
    }

    [Theory]
    [InlineData("1234567")]
    [InlineData("123456789")]
    [InlineData("ABCDE-FGH")]
    [InlineData("12.345-67")]
    public void CreateZipCode_InvalidValue_ShouldThrowArgumentException(string value)
    {
      var ex = Assert.Throws<ArgumentException>(() => new ZipCode(value));
      Assert.Equal("Invalid ZipCode.", ex.Message);
    }

    [Fact]
    public void ZipCodes_WithSameValue_ShouldBeEqual()
    {
      var zip1 = new ZipCode("12345-678");
      var zip2 = new ZipCode("12345678");

      Assert.Equal(zip1, zip2);
      Assert.True(zip1.Equals(zip2));
      Assert.Equal(zip1.GetHashCode(), zip2.GetHashCode());
    }

    [Fact]
    public void ZipCodes_WithDifferentValues_ShouldNotBeEqual()
    {
      var zip1 = new ZipCode("12345678");
      var zip2 = new ZipCode("87654321");

      Assert.NotEqual(zip1, zip2);
      Assert.False(zip1.Equals(zip2));
    }

    [Fact]
    public void ToString_ShouldReturnZipCodeValue()
    {
      var zipCode = new ZipCode("12345-678");
      Assert.Equal("12345678", zipCode.ToString());
    }
  }
}
