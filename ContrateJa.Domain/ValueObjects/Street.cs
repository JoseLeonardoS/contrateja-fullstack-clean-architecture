using System.Text.RegularExpressions;

namespace ContrateJa.Domain.ValueObjects
{
  public sealed class Street : IEquatable<Street>
  {
    public string Name { get; }

    public Street(string name)
    {
      if (string.IsNullOrWhiteSpace(name))
        throw new ArgumentException("Street is required.");

      name = name.Trim();

      if (name.Length < 3 || name.Length > 150)
        throw new ArgumentException("Invalid street name.");

      if (!Regex.IsMatch(name, @"^[A-Za-zÀ-ÖØ-öø-ÿ0-9]+([ '.\-][A-Za-zÀ-ÖØ-öø-ÿ0-9]+)*$"))
        throw new ArgumentException("Invalid street name.");

      Name = name;
    }

    public bool Equals(Street? other)
        => other is not null && Name == other.Name;

    public override bool Equals(object? obj)
        => Equals(obj as Street);

    public override int GetHashCode()
        => Name.GetHashCode();

    public override string ToString()
        => Name;
  }
}
