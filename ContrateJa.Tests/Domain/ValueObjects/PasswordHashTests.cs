using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Tests.Domain.ValueObjects
{
  public sealed class PasswordHashTests
  {
    public static IEnumerable<object[]> ValidPlainPasswords()
    {
      yield return new object[] { "Aa1!aaaa" };
      yield return new object[] { "StrongP@ssw0rd!" };
      yield return new object[] { "Zz9#Zz9#Zz9#" };
      yield return new object[] { "A1!aaaaaA1!aaaaa" };
      yield return new object[] { "Abcdef1!Abcdef1!" };
    }

    public static IEnumerable<object[]> InvalidPlainPasswords_Format()
    {
      yield return new object[] { "Aa1aaaaa" };
      yield return new object[] { "aa1!aaaa" };
      yield return new object[] { "AA1!AAAA" };
      yield return new object[] { "Aa!aaaaa" };
      yield return new object[] { "Aa1!aaa" };
      yield return new object[] { new string('A', 65) + "a1!" };
    }

    public static IEnumerable<object[]> InvalidPlainPasswords_EmptyOrWhitespace()
    {
      yield return new object[] { "" };
      yield return new object[] { " " };
      yield return new object[] { "   " };
      yield return new object[] { "\t" };
      yield return new object[] { "\n" };
      yield return new object[] { "\r\n" };
    }

    [Fact]
    public void Create_WithNull_Throws()
    {
      Assert.Throws<ArgumentException>(() => PasswordHash.Create(null));
    }

    [Theory]
    [MemberData(nameof(InvalidPlainPasswords_EmptyOrWhitespace))]
    public void Create_WithEmptyOrWhitespace_ThrowsArgumentException(string value)
    {
      Assert.Throws<ArgumentException>(() => PasswordHash.Create(value));
    }

    [Theory]
    [MemberData(nameof(InvalidPlainPasswords_Format))]
    public void Create_WithInvalidFormat_ThrowsArgumentException(string value)
    {
      Assert.Throws<ArgumentException>(() => PasswordHash.Create(value));
    }

    [Theory]
    [MemberData(nameof(ValidPlainPasswords))]
    public void Create_WithValidPassword_ReturnsHashedValue(string plain)
    {
      var password = PasswordHash.Create(plain);

      Assert.NotNull(password);
      Assert.False(string.IsNullOrWhiteSpace(password.Value));
      Assert.NotEqual(plain, password.Value);
      Assert.Contains("$2", password.Value);
      Assert.True(password.Value.Length > 20);
    }

    [Theory]
    [MemberData(nameof(ValidPlainPasswords))]
    public void Verify_WithCorrectPassword_ReturnsTrue(string plain)
    {
      var password = PasswordHash.Create(plain);

      Assert.True(password.Verify(plain));
    }

    [Theory]
    [MemberData(nameof(ValidPlainPasswords))]
    public void Verify_WithWrongPassword_ReturnsFalse(string plain)
    {
      var password = PasswordHash.Create(plain);

      Assert.False(password.Verify(plain + "X"));
    }
  }
}