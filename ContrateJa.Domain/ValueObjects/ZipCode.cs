using System.Text.RegularExpressions;

namespace ContrateJa.Domain.ValueObjects
{
  public sealed class ZipCode : IEquatable<ZipCode>
  {
    public string Value { get; }

    public ZipCode(string value)
    {
      if (string.IsNullOrWhiteSpace(value))
        throw new ArgumentException("ZipCode is required.");

      value = Regex.Replace(value, @"\D", "");

      if (!Regex.IsMatch(value, @"^\d{8}$"))
        throw new ArgumentException("Invalid ZipCode.");

      Value = value;
    }

    public bool Equals(ZipCode? other)
        => other is not null && Value == other.Value;

    public override bool Equals(object? obj)
        => Equals(obj as ZipCode);

    public override int GetHashCode()
        => Value.GetHashCode();

    public override string ToString()
        => Value;
  }
}
