namespace ContrateJa.Domain.ValueObjects;

public sealed class PasswordHash
{
    public string Hash { get; }

    public PasswordHash(string hash)
    {
        if(string.IsNullOrWhiteSpace(hash))
            throw  new ArgumentException("Password hash cannot be null or empty", nameof(hash));
        
        Hash = hash;
    }
}