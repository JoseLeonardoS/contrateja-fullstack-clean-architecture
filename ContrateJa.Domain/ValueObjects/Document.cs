using System.Text.RegularExpressions;

namespace ContrateJa.Domain.ValueObjects
{
  public sealed class Document : IEquatable<Document>
  {
    public string Value { get; }
    public EDocumentType Type { get; }

    private Document() { }
    
    public Document(string value)
    {
      if (string.IsNullOrWhiteSpace(value))
        throw new ArgumentException("Document is required.", nameof(value));

      value = NonDigit.Replace(value, "");

      if (value.Length == 11)
      {
        if (!IsValidCpf(value))
          throw new ArgumentException("Invalid CPF.", nameof(value));

        Type = EDocumentType.CPF;
      }
      else if (value.Length == 14)
      {
        if (!IsValidCnpj(value))
          throw new ArgumentException("Invalid CNPJ.", nameof(value));

        Type = EDocumentType.CNPJ;
      }
      else
      {
        throw new ArgumentException("Invalid document length.", nameof(value));
      }

      Value = value;
    }

    private static bool IsValidCpf(string cpf)
    {
      if (new string(cpf[0], cpf.Length) == cpf) return false;

      int[] mult1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
      int[] mult2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

      string temp = cpf[..9];
      int sum = 0;

      for (int i = 0; i < 9; i++)
        sum += (temp[i] - '0') * mult1[i];

      int mod = sum % 11;
      int digit1 = mod < 2 ? 0 : 11 - mod;

      temp += digit1;
      sum = 0;

      for (int i = 0; i < 10; i++)
        sum += (temp[i] - '0') * mult2[i];

      mod = sum % 11;
      int digit2 = mod < 2 ? 0 : 11 - mod;

      return cpf.EndsWith($"{digit1}{digit2}");
    }

    private static bool IsValidCnpj(string cnpj)
    {
      if (new string(cnpj[0], cnpj.Length) == cnpj) return false;

      int[] mult1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
      int[] mult2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

      string temp = cnpj[..12];
      int sum = 0;

      for (int i = 0; i < 12; i++)
        sum += (temp[i] - '0') * mult1[i];

      int mod = sum % 11;
      int digit1 = mod < 2 ? 0 : 11 - mod;

      temp += digit1;
      sum = 0;

      for (int i = 0; i < 13; i++)
        sum += (temp[i] - '0') * mult2[i];

      mod = sum % 11;
      int digit2 = mod < 2 ? 0 : 11 - mod;

      return cnpj.EndsWith($"{digit1}{digit2}");
    }

    public bool Equals(Document? other)
        => other is not null && Value == other.Value;

    public override bool Equals(object? obj)
        => Equals(obj as Document);

    public override int GetHashCode()
        => Value.GetHashCode(StringComparison.Ordinal);

    public override string ToString()
        => Value;
    
    public static bool operator ==(Document? left, Document? right) => left?.Equals(right) ?? right is null;
    public static bool operator !=(Document? left, Document? right) => !(left == right);

    private static readonly Regex NonDigit = new Regex(@"\D", RegexOptions.Compiled);
  }
}
