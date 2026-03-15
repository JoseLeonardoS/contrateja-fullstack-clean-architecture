using System.Text.RegularExpressions;
using ContrateJa.Domain.Enums;

namespace ContrateJa.Domain.ValueObjects
{
  public sealed class Phone : IEquatable<Phone>
  {
    public ECountryCode CountryCode { get; }
    public string NationalNumber { get; }
    public string E164 { get; }
    
    private Phone() { }

    public Phone(ECountryCode countryCode, string nationalNumber)
    {
      if (!Enum.IsDefined(countryCode))
        throw new ArgumentException("Invalid country code.", nameof(countryCode));

      if (string.IsNullOrWhiteSpace(nationalNumber))
        throw new ArgumentException("Phone number is required.", nameof(nationalNumber));

      var digits = NonDigits.Replace(nationalNumber, "");

      var countryDigits = ((int)countryCode).ToString();

      if (digits.Length < 5)
        throw new ArgumentException("Invalid phone number.", nameof(nationalNumber));

      if ((countryDigits.Length + digits.Length) > 15)
        throw new ArgumentException("Phone number exceeds E.164 limit.", nameof(nationalNumber));

      CountryCode = countryCode;
      NationalNumber = digits;
      E164 = $"+{countryDigits}{digits}";
    }

    public override string ToString() => E164;

    public bool Equals(Phone? other)
      => other is not null && E164 == other.E164;

    public override bool Equals(object? obj)
      => Equals(obj as Phone);

    public override int GetHashCode()
      => E164.GetHashCode(StringComparison.Ordinal);
    
    public static bool operator ==(Phone? left,  Phone? right) => left?.Equals(right) ?? right is null;
    public static bool operator !=(Phone? left,  Phone? right) => !(left == right);
    
    private static readonly Regex NonDigits= new Regex(@"\D", RegexOptions.Compiled);
  }
}
