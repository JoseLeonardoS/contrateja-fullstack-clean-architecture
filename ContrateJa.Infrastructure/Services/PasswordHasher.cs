using ContrateJa.Application.Abstractions.Services;

namespace ContrateJa.Infrastructure.Services;

public sealed class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
        => BCrypt.Net.BCrypt.HashPassword(password);

    public bool Validate(string password, string hash)
        => BCrypt.Net.BCrypt.Verify(password, hash);
}