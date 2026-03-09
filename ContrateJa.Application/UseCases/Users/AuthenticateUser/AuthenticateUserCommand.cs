namespace ContrateJa.Application.UseCases.Users.AuthenticateUser;

public sealed class AuthenticateUserCommand
{
    public string Email { get; }
    public string Password { get; }
    
    public AuthenticateUserCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }
}