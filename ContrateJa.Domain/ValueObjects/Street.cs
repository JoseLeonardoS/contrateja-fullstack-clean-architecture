using System.Text.RegularExpressions;

namespace ContrateJa.Domain.ValueObjects
{
  public sealed class Street : IEquatable<Street>
  {
    public string Name { get; }
    
    private Street() { }

    public Street(string name)
    {
      if (string.IsNullOrWhiteSpace(name))
        throw new ArgumentException("Street is required.", nameof(name));

      name = name.Trim();

      if (name.Length is < 3 or > 150)
        throw new ArgumentException("Invalid street name.", nameof(name));

      if (!ValidName.IsMatch(name))
        throw new ArgumentException("Invalid street name.", nameof(name));

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
    
    public static bool operator ==(Street? left, Street? right) => left?.Equals(right) ?? right is null;
    public static bool operator !=(Street? left, Street? right) => !(left == right);
    
    private static readonly Regex ValidName = new Regex(@"^[A-Za-zÀ-ÖØ-öø-ÿ0-9]+([ '.\-]+[A-Za-zÀ-ÖØ-öø-ÿ0-9]+)*$", RegexOptions.Compiled);
  }
}
