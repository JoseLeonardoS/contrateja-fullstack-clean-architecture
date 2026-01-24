using System.Text.RegularExpressions;

namespace ContrateJa.Domain.ValueObjects
{
  public sealed class City : IEquatable<City>
  {
    public string Name { get; }

    public City(string name)
    {
      if (string.IsNullOrWhiteSpace(name))
        throw new ArgumentException("City is required.");

      name = name.Trim();

      if (name.Length < 2 || name.Length > 100)
        throw new ArgumentException("Invalid city name.");

      if (!Regex.IsMatch(name, @"^[A-Za-zÀ-ÖØ-öø-ÿ]+([ '-][A-Za-zÀ-ÖØ-öø-ÿ]+)*$"))
        throw new ArgumentException("Invalid city name.");

      Name = name;
    }

    public bool Equals(City? other)
        => other is not null && Name == other.Name;

    public override bool Equals(object? obj)
        => Equals(obj as City);

    public override int GetHashCode()
        => Name.GetHashCode();

    public override string ToString()
        => Name;
  }
}
