using ContrateJa.Domain.Enums;
using ContrateJa.Domain.ValueObjects;
using Xunit;

namespace ContrateJa.Tests.Domain.ValueObjects
{
  public class PhoneTests
  {
    [Fact]
    public void Phone_WithValidData_ShouldCreateSuccessfully()
    {
      var phone = new Phone(ECountryCode.BR, "11987654321");

      Assert.Equal(ECountryCode.BR, phone.CountryCode);
      Assert.Equal("11987654321", phone.NationalNumber);
      Assert.Equal("+5511987654321", phone.E164);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Phone_WithNullOrEmptyNumber_ShouldThrow(string? number)
    {
      var ex = Assert.Throws<ArgumentException>(() =>
        new Phone(ECountryCode.BR, number!)
      );

      Assert.Equal("Phone number is required.", ex.Message);
    }

    [Fact]
    public void Phone_WithInvalidCountryCode_ShouldThrow()
    {
      var ex = Assert.Throws<ArgumentException>(() =>
        new Phone((ECountryCode)999, "11987654321")
      );

      Assert.Equal("Invalid country code.", ex.Message);
    }

    [Fact]
    public void Phone_WithTooShortNationalNumber_ShouldThrow()
    {
      var ex = Assert.Throws<ArgumentException>(() =>
        new Phone(ECountryCode.BR, "1234")
      );

      Assert.Equal("Invalid phone number.", ex.Message);
    }

    [Fact]
    public void Phone_ExceedingE164Limit_ShouldThrow()
    {
      // 55 + 14 digits = 16 (invalid)
      var ex = Assert.Throws<ArgumentException>(() =>
        new Phone(ECountryCode.BR, "12345678901234")
      );

      Assert.Equal("Phone number exceeds E.164 limit.", ex.Message);
    }

    [Fact]
    public void Phone_ShouldNormalizeNonDigits()
    {
      var phone = new Phone(ECountryCode.US, "(415) 555-2671");

      Assert.Equal("4155552671", phone.NationalNumber);
      Assert.Equal("+14155552671", phone.E164);
    }

    [Fact]
    public void Phone_WithSameValues_ShouldBeEqual()
    {
      var phone1 = new Phone(ECountryCode.PT, "912345678");
      var phone2 = new Phone(ECountryCode.PT, "912345678");

      Assert.Equal(phone1, phone2);
    }

    [Fact]
    public void Phone_WithDifferentValues_ShouldNotBeEqual()
    {
      var phone1 = new Phone(ECountryCode.FR, "612345678");
      var phone2 = new Phone(ECountryCode.FR, "698765432");

      Assert.NotEqual(phone1, phone2);
    }

    [Fact]
    public void Phone_ToString_ShouldReturnE164()
    {
      var phone = new Phone(ECountryCode.UK, "7123456789");

      Assert.Equal(phone.E164, phone.ToString());
    }
  }
}
