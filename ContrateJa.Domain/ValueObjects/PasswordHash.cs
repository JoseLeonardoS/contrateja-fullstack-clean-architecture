namespace ContrateJa.Domain.ValueObjects;

public sealed class PasswordHash
{
    public string Hash { get; }
    
    private PasswordHash() { }

    public PasswordHash(string hash)
    {
        if(string.IsNullOrWhiteSpace(hash))
            throw  new ArgumentException("Password hash cannot be null or empty", nameof(hash));
        
        Hash = hash;
    }
}