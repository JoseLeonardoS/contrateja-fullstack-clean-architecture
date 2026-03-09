using System.Text.RegularExpressions;

namespace ContrateJa.Domain.ValueObjects
{
    public sealed class Password
    {
        public string Value { get; }

        public Password(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Password cannot be null or empty.", nameof(value));

            if (value.Length is < 8 or > 64)
                throw new ArgumentException("Password length must be between 8 and 64 characters.", nameof(value));

            if (!Uppercase.IsMatch(value))
                throw new ArgumentException("Password must contain at least one uppercase letter.", nameof(value));

            if (!Lowercase.IsMatch(value))
                throw new ArgumentException("Password must contain at least one lowercase letter.", nameof(value));

            if (!Digit.IsMatch(value))
                throw new ArgumentException("Password must contain at least one digit.", nameof(value));

            if (!Special.IsMatch(value))
                throw new ArgumentException("Password must contain at least one special character.", nameof(value));

            Value = value;
        }

        private static readonly Regex Uppercase = new Regex("[A-Z]", RegexOptions.Compiled);
        private static readonly Regex Lowercase = new Regex("[a-z]", RegexOptions.Compiled);
        private static readonly Regex Digit = new Regex("[0-9]", RegexOptions.Compiled);
        private static readonly Regex Special = new Regex(@"[\W_]", RegexOptions.Compiled);
    }
}