namespace ContrateJa.Domain.ValueObjects
{
    public sealed class State : IEquatable<State>
    {
        private static readonly HashSet<string> ValidStates =
        [
            "AC","AL","AP","AM","BA","CE","DF","ES","GO","MA","MT","MS",
            "MG","PA","PB","PR","PE","PI","RJ","RN","RS","RO","RR","SC",
            "SP","SE","TO"
        ];

        public string Code { get; }

        public State(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("State is required.", nameof(code));

            code = code.Trim().ToUpper();

            if (!ValidStates.Contains(code))
                throw new ArgumentException("Invalid state.", nameof(code));

            Code = code;
        }

        public bool Equals(State? other)
            => other is not null && Code == other.Code;

        public override bool Equals(object? obj)
            => Equals(obj as State);

        public override int GetHashCode()
            => Code.GetHashCode(StringComparison.Ordinal);

        public override string ToString()
            => Code;
    
        public static bool operator ==(State? left, State? right) => left?.Equals(right) ?? right is null;
        public static bool operator !=(State? left, State? right) => !(left == right);
    }
}