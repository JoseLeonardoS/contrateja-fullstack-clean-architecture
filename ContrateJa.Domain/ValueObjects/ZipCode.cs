using System.Text.RegularExpressions;

namespace ContrateJa.Domain.ValueObjects
{
  public sealed class ZipCode : IEquatable<ZipCode>
  {
    public string Value { get; }
    
    private ZipCode() { }

    public ZipCode(string code)
    {
      if (string.IsNullOrWhiteSpace(code))
        throw new ArgumentException("ZipCode is required.", nameof(code));

      code = NonDigits.Replace(code, "");

      if (!ValidCode.IsMatch(code))
        throw new ArgumentException("Invalid ZipCode.", nameof(code));

      Value = code;
    }

    public bool Equals(ZipCode? other)
        => other is not null && Value == other.Value;

    public override bool Equals(object? obj)
        => Equals(obj as ZipCode);

    public override int GetHashCode()
        => Value.GetHashCode(StringComparison.Ordinal);

    public override string ToString()
        => Value;
    
    public static bool operator ==(ZipCode? left, ZipCode? right) => left?.Equals(right) ?? right is null;
    public static bool operator !=(ZipCode? left, ZipCode? right) => !(left == right);
    
    private static readonly Regex NonDigits = new Regex(@"\D", RegexOptions.Compiled);
    private static readonly Regex ValidCode = new Regex(@"^\d{8}$", RegexOptions.Compiled);
  }
}
