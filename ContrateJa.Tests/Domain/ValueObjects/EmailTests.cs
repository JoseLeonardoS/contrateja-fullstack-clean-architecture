using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Tests.Domain.ValueObjects
{

  public class EmailTests
  {
    [Fact]
    public void Email_WhithSameValues_ShouldReturnSuccess()
    {
      var email1 = new Email("test@example.com");
      var email2 = new Email("test@example.com");

      Assert.Equal(email1, email2);
    }

    [Fact]
    public void Email_WhithDiferentValues_ShouldReturnSuccess()
    {
      var email1 = new Email("test@example.com");
      var email2 = new Email("diferent@example.com");

      Assert.NotEqual(email1, email2);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Email_WhithNullOrEmpty_ShouldThrowArgumentException(string? invalidEmail)
    {
      var ex = Assert.Throws<ArgumentException>(() => new Email(invalidEmail!));
      Assert.Equal("Email address cannot be empty.", ex.Message);
    }

    [Fact]
    public void Email_WhithManyCharacters_ShouldThrowArgumentException()
    {
      var longEmail = new string('a', 244) + "@example.com";

      var ex = Assert.Throws<ArgumentException>(() => new Email(longEmail));
      Assert.Equal("Email address is too long.", ex.Message);
    }

    [Fact]
    public void Email_WhithFewCharacters_ShouldThrowArgumentException()
    {
      var shortEmail = "a@b";

      var ex = Assert.Throws<ArgumentException>(() => new Email(shortEmail));
      Assert.Equal("Email address is too short.", ex.Message);
    }

    [Theory]
    [InlineData("invalidemail")]
    [InlineData("invalid@ email.com")]
    [InlineData("invalid@.com")]
    [InlineData("invalid@com")]
    public void Email_WhithInvalidFormat_ShouldThrowArgumentException(string invalidEmail)
    {
      var ex = Assert.Throws<ArgumentException>(() => new Email(invalidEmail));
      Assert.Equal("Invalid email address.", ex.Message);
    }

    [Theory]
    [InlineData("test@example.com")]
    [InlineData("user.name@example.com")]
    [InlineData("user+tag@example.com")]
    public void Email_WithValidFormat_ShouldCreateEmail(string validEmail)
    {
      var email = new Email(validEmail);

      Assert.Equal(validEmail.Trim().ToLower(), email.Address);
    }
  }
}
