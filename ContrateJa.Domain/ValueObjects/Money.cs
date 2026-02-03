using System.Globalization;

namespace ContrateJa.Domain.ValueObjects
{
  public sealed class Money : IEquatable<Money>
  {
    public decimal Amount { get; }

    public Money(decimal amount)
    {
      if (amount < 0)
        throw new ArgumentException("Amount cannot be negative.", nameof(amount));

      Amount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);
    }

    public override string ToString()
      => Amount.ToString("F2", CultureInfo.InvariantCulture);

    public bool Equals(Money? other)
      => other is not null && other.Amount == Amount;

    public override bool Equals(object? obj)
      => Equals(obj as Money);

    public override int GetHashCode()
      => Amount.GetHashCode();
  }
}
