namespace ContrateJa.Domain.ValueObjects
{
  public sealed class Area : IEquatable<Area>
  {
    public State State { get; }
    public City City { get; }

    public Area(State state, City city)
    {
      State = state ?? throw new ArgumentNullException(nameof(state));
      City = city ?? throw new ArgumentNullException(nameof(city));
    }

    public override string ToString()
      => $"{City}, {State}";

    public bool Equals(Area? other)
      => other is not null &&
              State.Equals(other.State) &&
              City.Equals(other.City);

    public override bool Equals(object? obj)
      => Equals(obj as Area);

    public override int GetHashCode()
      => HashCode.Combine(State, City);
    
    public static bool operator ==(Area? left, Area? right) => left?.Equals(right) ?? right is null;
    public static bool operator !=(Area? left, Area? right) => !(left == right);
  }
}
