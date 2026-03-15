using System.Text.RegularExpressions;

namespace ContrateJa.Domain.ValueObjects
{
  public sealed class Email : IEquatable<Email>
  {
    public string Address { get; }

    private Email() { }
    
    public Email(string address)
    {
      if (string.IsNullOrWhiteSpace(address))
        throw new ArgumentException("Email address cannot be empty.", nameof(address));

      address = address.Trim().ToLower();

      if (address.Length > 255)
        throw new ArgumentException("Email address is too long.", nameof(address));
      
      if (address.Length < 5)
        throw new ArgumentException("Email address is too short.", nameof(address));

      if (!ValidEmail.IsMatch(address))
        throw new ArgumentException("Invalid email address.", nameof(address));

      Address = address;
    }

    public bool Equals(Email? other)
    {
      if (other is null) return false;
      return Address == other.Address;
    }

    public override bool Equals(object? obj)
        => Equals(obj as Email);

    public override int GetHashCode()
        => Address.GetHashCode();
    
    public static bool operator ==(Email? left, Email? right) => left?.Equals(right) ?? right is null;
    public static bool operator !=(Email? left, Email? right) => !(left == right);

    private static readonly Regex ValidEmail =
      new Regex(@"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$", RegexOptions.Compiled);
  }
}