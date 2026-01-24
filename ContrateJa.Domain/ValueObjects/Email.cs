using System.Text.RegularExpressions;

namespace ContrateJa.Domain.ValueObjects
{
  public sealed class Email : IEquatable<Email>
  {
    public string Address { get; }

    public Email(string address)
    {
      if (string.IsNullOrWhiteSpace(address))
        throw new ArgumentException("Email address cannot be empty.");

      address = address.Trim().ToLower();

      if (address.Length > 255)
        throw new ArgumentException("Email address is too long.");
      else if (address.Length < 5)
        throw new ArgumentException("Email address is too short.");

      if (address.Contains(" "))
        throw new ArgumentException("Invalid email address.");

      if (!Regex.IsMatch(address, @"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$"))
        throw new ArgumentException("Invalid email address.");

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
  }
}