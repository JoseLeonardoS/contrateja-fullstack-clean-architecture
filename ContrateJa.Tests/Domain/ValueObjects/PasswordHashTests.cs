using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Tests.Domain.ValueObjects;

public sealed class PasswordHashTests
{
    public static IEnumerable<object[]> NullOrWhitespaceValues()
    {
        yield return new object[] { null! };
        yield return new object[] { "" };
        yield return new object[] { "   " };
    }

    [Theory]
    [MemberData(nameof(NullOrWhitespaceValues))]
    public void Constructor_WithNullOrWhitespace_ThrowsArgumentException(string? value)
    {
        Assert.Throws<ArgumentException>(() => new PasswordHash(value!));
    }

    [Fact]
    public void Constructor_WithValidHash_DoesNotThrow()
    {
        var exception = Record.Exception(() => new PasswordHash("$2a$11$validhashstring"));
        Assert.Null(exception);
    }

    [Fact]
    public void Constructor_WithValidHash_SetsHashProperty()
    {
        var hash = "$2a$11$validhashstring";
        var passwordHash = new PasswordHash(hash);

        Assert.Equal(hash, passwordHash.Hash);
    }
}