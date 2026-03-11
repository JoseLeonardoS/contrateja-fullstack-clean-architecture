using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ContrateJa.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ContrateJa.Infrastructure.Services;

public sealed class TokenGenerator : ITokenGenerator
{
    private readonly IConfiguration _configuration;
        
    public TokenGenerator(IConfiguration configuration)
        => _configuration = configuration;
    
    public string GenerateToken(long userId, string email, string accountType)
    {
        
        var claims = new []
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, accountType)
        };
        
        var secret = _configuration["Jwt:Secret"]
            ?? throw new InvalidOperationException("Jwt:Secret is not configured.");
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret!));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expires = int.Parse(_configuration["Jwt:ExpiresInMinutes"]
            ?? throw new InvalidOperationException("Jwt:ExpiresInMinutes is not configured."));

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(expires),
            SigningCredentials =  credentials
        };

        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateEncodedJwt(descriptor);
        return token;
    }
}