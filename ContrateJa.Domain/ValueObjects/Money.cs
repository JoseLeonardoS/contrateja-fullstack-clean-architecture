using System.Globalization;

namespace ContrateJa.Domain.ValueObjects
{
  public sealed class Money : IEquatable<Money>
  {
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
      if (amount < 0)
        throw new ArgumentException("Amount cannot be negative.", nameof(amount));

      if (string.IsNullOrWhiteSpace(currency))
        throw new ArgumentException("Currency cannot be null or empty.", nameof(currency));

      if (currency.Length != 3)
        throw new ArgumentException("Currency must be a 3-letter ISO 4217 code.", nameof(currency));
      
      if (!currency.All(char.IsLetter))
        throw new ArgumentException("Currency must contain only letters.", nameof(currency));

      Amount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);
      Currency = currency.ToUpperInvariant();
    }

    public override string ToString()
      => $"{Currency} {Amount.ToString("F2", CultureInfo.InvariantCulture)}";

    public bool Equals(Money? other)
      => other is not null && Amount == other.Amount && Currency == other.Currency;

    public override bool Equals(object? obj)
      => Equals(obj as Money);

    public static bool operator ==(Money? left, Money? right) => left?.Equals(right) ?? right is null;
    public static bool operator !=(Money? left, Money? right) => !(left == right);

    public override int GetHashCode()
      => HashCode.Combine(Amount, Currency);
  }
}
