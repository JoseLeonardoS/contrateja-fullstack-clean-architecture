using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Tests.Domain.ValueObjects
{
    public sealed class PasswordTests
    {
        public static IEnumerable<object[]> NullOrWhitespaceValues()
        {
            yield return new object[] { null! };
            yield return new object[] { "" };
            yield return new object[] { "   " };
        }

        [Theory]
        [MemberData(nameof(NullOrWhitespaceValues))]
        public void Constructor_WithNullOrWhitespace_ThrowsArgumentException(string value)
        {
            Assert.Throws<ArgumentException>(() => new Password(value));
        }

        public static IEnumerable<object[]> TooShortPasswords()
        {
            yield return new object[] { "Aa1!" };
            yield return new object[] { "Aa1!aaa" };
        }

        [Theory]
        [MemberData(nameof(TooShortPasswords))]
        public void Constructor_WithTooShortPassword_ThrowsArgumentException(string value)
        {
            var ex = Assert.Throws<ArgumentException>(() => new Password(value));
            Assert.Equal("value", ex.ParamName);
        }

        [Fact]
        public void Constructor_WithTooLongPassword_ThrowsArgumentException()
        {
            var value = "Aa1!" + new string('a', 61);
            var ex = Assert.Throws<ArgumentException>(() => new Password(value));
            Assert.Equal("value", ex.ParamName);
        }

        [Fact]
        public void Constructor_WithoutUppercase_ThrowsArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => new Password("aa1!aaaa"));
            Assert.Equal("value", ex.ParamName);
        }

        [Fact]
        public void Constructor_WithoutLowercase_ThrowsArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => new Password("AA1!AAAA"));
            Assert.Equal("value", ex.ParamName);
        }

        [Fact]
        public void Constructor_WithoutDigit_ThrowsArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => new Password("Aa!!aaaa"));
            Assert.Equal("value", ex.ParamName);
        }

        [Fact]
        public void Constructor_WithoutSpecialCharacter_ThrowsArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => new Password("Aa1aaaaa"));
            Assert.Equal("value", ex.ParamName);
        }

        public static IEnumerable<object[]> ValidPasswords()
        {
            yield return new object[] { "Aa1!aaaa" };
            yield return new object[] { "Aa1!" + new string('a', 60) };
            yield return new object[] { "Senha@123" };
            yield return new object[] { "P@ssw0rd" };
            yield return new object[] { "Teste_123" };
        }

        [Theory]
        [MemberData(nameof(ValidPasswords))]
        public void Constructor_WithValidPassword_DoesNotThrow(string value)
        {
            var exception = Record.Exception(() => new Password(value));
            Assert.Null(exception);
        }

        [Fact]
        public void Constructor_WithExactly8Characters_DoesNotThrow()
        {
            var exception = Record.Exception(() => new Password("Aa1!aaaa"));
            Assert.Null(exception);
        }

        [Fact]
        public void Constructor_WithExactly64Characters_DoesNotThrow()
        {
            var value = "Aa1!" + new string('a', 60);
            var exception = Record.Exception(() => new Password(value));
            Assert.Null(exception);
        }

        [Fact]
        public void Equals_Null_ReturnsFalse()
        {
            var a = new Password("Aa1!aaaa");
            Assert.False(a.Equals(null));
        }

        [Fact]
        public void Equals_ObjectOverload_WithDifferentType_ReturnsFalse()
        {
            var a = new Password("Aa1!aaaa");
            Assert.False(a.Equals("Aa1!aaaa"));
        }
    }
}