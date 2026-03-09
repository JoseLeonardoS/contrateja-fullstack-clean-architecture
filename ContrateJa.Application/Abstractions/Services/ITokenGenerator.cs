namespace ContrateJa.Application.Abstractions.Services;

public interface ITokenGenerator
{
    string GenerateToken(long userId, string email, string accountType);
}