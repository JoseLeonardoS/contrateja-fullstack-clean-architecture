using System.Text.RegularExpressions;

namespace ContrateJa.Domain.ValueObjects
{
  public sealed class Name : IEquatable<Name>
  {
    public string FirstName { get; }
    public string LastName { get; }
    public string FullName { get; }

    public Name(string firstName, string lastName)
    {
      FirstName = Validate(firstName, "First name");
      LastName = Validate(lastName, "Last name");
      var value = $"{FirstName} {LastName}";

      if (value.Length > 150)
        throw new ArgumentException("Name is too long.");

      FullName = value;

    }

    public override string ToString()
      => FullName;

    private static string Validate(string value, string fieldName)
    {
      if (string.IsNullOrWhiteSpace(value))
        throw new ArgumentException($"{fieldName} cannot be empty.");

      value = value.Trim();

      if (value.Length < 3)
        throw new ArgumentException($"{fieldName} is too short.");

      if (value.Any(char.IsDigit))
        throw new ArgumentException($"{fieldName} cannot contain numbers.");

      if (!Regex.IsMatch(value, @"^[A-Za-zÀ-ÖØ-öø-ÿ]+([ '-][A-Za-zÀ-ÖØ-öø-ÿ]+)*$"))
        throw new ArgumentException($"{fieldName} contains invalid characters.");

      return value;
    }

    public bool Equals(Name? other)
    {
      return other is not null &&
             FirstName == other.FirstName &&
             LastName == other.LastName;
    }

    public override bool Equals(object? obj)
        => Equals(obj as Name);

    public override int GetHashCode()
        => HashCode.Combine(FirstName, LastName);
  }
}