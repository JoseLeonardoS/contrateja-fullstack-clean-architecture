using System.Text.RegularExpressions;

namespace ContrateJa.Domain.ValueObjects
{
  public sealed class City : IEquatable<City>
  {
    public string Name { get; }

    private City() { }

    public City(string name)
    {
      if (string.IsNullOrWhiteSpace(name))
        throw new ArgumentException("City is required.", nameof(name));

      name = name.Trim();

      if (name.Length is < 2 or > 100)
        throw new ArgumentException("Invalid city name.", nameof(name));

      if (!ValidName.IsMatch(name))
        throw new ArgumentException("Invalid city name.", nameof(name));

      Name = name;
    }

    public bool Equals(City? other)
        => other is not null && Name == other.Name;

    public override bool Equals(object? obj)
        => Equals(obj as City);

    public override int GetHashCode()
        => Name.GetHashCode(StringComparison.OrdinalIgnoreCase);

    public override string ToString()
        => Name;
    
    public static bool operator ==(City? left, City? right) => left?.Equals(right) ?? right is null;
    public static bool operator !=(City? left, City? right) => !(left == right);
    
    private static readonly Regex ValidName = new Regex(
      @"^[A-Za-zÀ-ÖØ-öø-ÿ]+([ '-][A-Za-zÀ-ÖØ-öø-ÿ]+)*$",
      RegexOptions.Compiled);
  }
}
