using System.Globalization;
using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Tests.Domain.ValueObjects
{
  public sealed class MoneyTests
  {
    [Theory]
    [InlineData(-0.01)]
    [InlineData(-1.00)]
    [InlineData(-999999.99)]
    public void Ctor_WithNegativeAmount_ThrowsArgumentException(decimal amount)
    {
      var ex = Assert.Throws<ArgumentException>(() => new Money(amount));
      Assert.Equal("amount", ex.ParamName);
    }

    [Theory]
    [InlineData(0.00, 0.00)]
    [InlineData(1.00, 1.00)]
    [InlineData(10.10, 10.10)]
    public void Ctor_SetsAmount(decimal input, decimal expected)
    {
      var money = new Money(input);
      Assert.Equal(expected, money.Amount);
    }

    [Theory]
    [InlineData(1.234, 1.23)]
    [InlineData(1.235, 1.24)]
    [InlineData(1.239, 1.24)]
    [InlineData(2.005, 2.01)]
    [InlineData(2.004, 2.00)]
    public void Ctor_RoundsToTwoDecimals_AwayFromZero(decimal input, decimal expected)
    {
      var money = new Money(input);
      Assert.Equal(expected, money.Amount);
    }

    [Theory]
    [InlineData(0, "0.00")]
    [InlineData(1, "1.00")]
    [InlineData(10.1, "10.10")]
    [InlineData(1234.5, "1234.50")]
    public void ToString_UsesInvariantCultureWithTwoDecimals(decimal input, string expected)
    {
      var originalCulture = CultureInfo.CurrentCulture;
      var originalUICulture = CultureInfo.CurrentUICulture;

      try
      {
        // Force a culture that uses comma as decimal separator
        CultureInfo.CurrentCulture = new CultureInfo("pt-BR");
        CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");

        var money = new Money(input);

        Assert.Equal(expected, money.ToString());
      }
      finally
      {
        CultureInfo.CurrentCulture = originalCulture;
        CultureInfo.CurrentUICulture = originalUICulture;
      }
    }

    [Fact]
    public void Equals_WithSameAmount_ReturnsTrue()
    {
      var a = new Money(10.10m);
      var b = new Money(10.10m);

      Assert.True(a.Equals(b));
      Assert.True(a.Equals((object)b));
    }

    [Fact]
    public void Equals_WithDifferentAmount_ReturnsFalse()
    {
      var a = new Money(10.10m);
      var b = new Money(10.11m);

      Assert.False(a.Equals(b));
      Assert.False(a.Equals((object)b));
    }

    [Fact]
    public void Equals_WithNull_ReturnsFalse()
    {
      var a = new Money(10.10m);

      Assert.False(a.Equals((Money?)null));
      Assert.False(a.Equals((object?)null));
    }

    [Fact]
    public void GetHashCode_SameAmount_SameHashCode()
    {
      var a = new Money(99.99m);
      var b = new Money(99.99m);

      Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    [Theory]
    [InlineData(1.234, 1.23)]
    [InlineData(1.235, 1.24)]
    public void Equality_BehavesCorrectlyAfterRounding(decimal input, decimal expectedRounded)
    {
      var a = new Money(input);
      var b = new Money(expectedRounded);

      Assert.True(a.Equals(b));
      Assert.Equal(expectedRounded, a.Amount);
    }
  }
}