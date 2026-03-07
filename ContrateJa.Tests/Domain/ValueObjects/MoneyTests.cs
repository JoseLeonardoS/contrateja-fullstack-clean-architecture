using System.Globalization;
using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Tests.Domain.ValueObjects;

public sealed class MoneyTests
{
    [Theory]
    [InlineData(-0.01)]
    [InlineData(-1.00)]
    [InlineData(-999999.99)]
    public void Constructor_WithNegativeAmount_ThrowsArgumentException(decimal amount)
    {
        var ex = Assert.Throws<ArgumentException>(() => new Money(amount, "BRL"));
        Assert.Equal("amount", ex.ParamName);
    }

    public static IEnumerable<object[]> NullOrWhitespaceCurrencies()
    {
        yield return new object[] { null! };
        yield return new object[] { "" };
        yield return new object[] { "   " };
    }

    [Theory]
    [MemberData(nameof(NullOrWhitespaceCurrencies))]
    public void Constructor_WithNullOrWhitespaceCurrency_ThrowsArgumentException(string? currency)
    {
        var ex = Assert.Throws<ArgumentException>(() => new Money(100, currency!));
        Assert.Equal("currency", ex.ParamName);
    }

    public static IEnumerable<object[]> InvalidCurrencyCodes()
    {
        yield return new object[] { "BR" };
        yield return new object[] { "BRLL" };
        yield return new object[] { "B" };
    }

    [Theory]
    [MemberData(nameof(InvalidCurrencyCodes))]
    public void Constructor_WithInvalidCurrencyLength_ThrowsArgumentException(string currency)
    {
        var ex = Assert.Throws<ArgumentException>(() => new Money(100, currency));
        Assert.Equal("currency", ex.ParamName);
    }

    [Fact]
    public void Constructor_NormalizesCurrencyToUppercase()
    {
        var money = new Money(100, "brl");
        Assert.Equal("BRL", money.Currency);
    }

    [Theory]
    [InlineData(0.00, 0.00)]
    [InlineData(1.00, 1.00)]
    [InlineData(10.10, 10.10)]
    public void Constructor_SetsAmount(decimal input, decimal expected)
    {
        var money = new Money(input, "BRL");
        Assert.Equal(expected, money.Amount);
    }

    [Theory]
    [InlineData(1.234, 1.23)]
    [InlineData(1.235, 1.24)]
    [InlineData(1.239, 1.24)]
    [InlineData(2.005, 2.01)]
    [InlineData(2.004, 2.00)]
    public void Constructor_RoundsToTwoDecimals_AwayFromZero(decimal input, decimal expected)
    {
        var money = new Money(input, "BRL");
        Assert.Equal(expected, money.Amount);
    }

    [Theory]
    [InlineData(0, "BRL 0.00")]
    [InlineData(1, "BRL 1.00")]
    [InlineData(10.1, "BRL 10.10")]
    [InlineData(1234.5, "BRL 1234.50")]
    public void ToString_IncludesCurrencyAndUsesInvariantCulture(decimal input, string expected)
    {
        var originalCulture = CultureInfo.CurrentCulture;
        var originalUICulture = CultureInfo.CurrentUICulture;

        try
        {
            CultureInfo.CurrentCulture = new CultureInfo("pt-BR");
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");

            var money = new Money(input, "BRL");
            Assert.Equal(expected, money.ToString());
        }
        finally
        {
            CultureInfo.CurrentCulture = originalCulture;
            CultureInfo.CurrentUICulture = originalUICulture;
        }
    }

    [Fact]
    public void Equals_SameAmountAndCurrency_ReturnsTrue()
    {
        var a = new Money(10.10m, "BRL");
        var b = new Money(10.10m, "BRL");

        Assert.True(a.Equals(b));
        Assert.True(a.Equals((object)b));
    }

    [Fact]
    public void Equals_DifferentAmount_ReturnsFalse()
    {
        var a = new Money(10.10m, "BRL");
        var b = new Money(10.11m, "BRL");

        Assert.False(a.Equals(b));
        Assert.False(a.Equals((object)b));
    }

    [Fact]
    public void Equals_DifferentCurrency_ReturnsFalse()
    {
        var a = new Money(10.10m, "BRL");
        var b = new Money(10.10m, "USD");

        Assert.False(a.Equals(b));
        Assert.False(a.Equals((object)b));
    }

    [Fact]
    public void Equals_WithNull_ReturnsFalse()
    {
        var a = new Money(10.10m, "BRL");

        Assert.False(a.Equals((Money?)null));
        Assert.False(a.Equals((object?)null));
    }

    [Fact]
    public void GetHashCode_SameAmountAndCurrency_ReturnsSameHash()
    {
        var a = new Money(99.99m, "BRL");
        var b = new Money(99.99m, "BRL");

        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    [Fact]
    public void GetHashCode_DifferentCurrency_ReturnsDifferentHash()
    {
        var a = new Money(99.99m, "BRL");
        var b = new Money(99.99m, "USD");

        Assert.NotEqual(a.GetHashCode(), b.GetHashCode());
    }

    [Theory]
    [InlineData(1.234, 1.23)]
    [InlineData(1.235, 1.24)]
    public void Equality_BehavesCorrectlyAfterRounding(decimal input, decimal expectedRounded)
    {
        var a = new Money(input, "BRL");
        var b = new Money(expectedRounded, "BRL");

        Assert.True(a.Equals(b));
        Assert.Equal(expectedRounded, a.Amount);
    }
}