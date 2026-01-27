using System.Text.RegularExpressions;

namespace ContrateJa.Domain.ValueObjects
{
  public sealed class PasswordHash : IEquatable<PasswordHash>
  {
    public string Value { get; }


    private PasswordHash(string hash)
    {
      Value = hash;
    }


    public static PasswordHash Create(string plainPassword)
    {
      ValidatePlainPassword(plainPassword);
      var hash = BCrypt.Net.BCrypt.HashPassword(plainPassword);
      return new PasswordHash(hash);
    }


    public static PasswordHash FromHash(string hashFromDatabase)
    {
      if (string.IsNullOrWhiteSpace(hashFromDatabase))
        throw new ArgumentException("Hash cannot be null or empty.", nameof(hashFromDatabase));


      if (!hashFromDatabase.StartsWith("$2", StringComparison.Ordinal))
        throw new ArgumentException("Hash format is invalid.", nameof(hashFromDatabase));


      return new PasswordHash(hashFromDatabase);
    }


    public bool Verify(string plainPassword)
    {
      if (string.IsNullOrWhiteSpace(plainPassword))
        return false;


      return BCrypt.Net.BCrypt.Verify(plainPassword, Value);
    }


    public bool Equals(PasswordHash? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is PasswordHash other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);


    private static void ValidatePlainPassword(string password)
    {
      if (string.IsNullOrWhiteSpace(password))
        throw new ArgumentException("Password cannot be null or empty.", nameof(password));


      if (password.Length < 8 || password.Length > 64)
        throw new ArgumentException("Password length must be between 8 and 64 characters.", nameof(password));


      if (!Uppercase.IsMatch(password))
        throw new ArgumentException("Password must contain at least one uppercase letter.", nameof(password));


      if (!Lowercase.IsMatch(password))
        throw new ArgumentException("Password must contain at least one lowercase letter.", nameof(password));


      if (!Digit.IsMatch(password))
        throw new ArgumentException("Password must contain at least one digit.", nameof(password));


      if (!Special.IsMatch(password))
        throw new ArgumentException("Password must contain at least one special character.", nameof(password));
    }


    private static readonly Regex Uppercase = new Regex("[A-Z]", RegexOptions.Compiled);
    private static readonly Regex Lowercase = new Regex("[a-z]", RegexOptions.Compiled);
    private static readonly Regex Digit = new Regex("[0-9]", RegexOptions.Compiled);
    private static readonly Regex Special = new Regex(@"[\W_]", RegexOptions.Compiled);
  }
}