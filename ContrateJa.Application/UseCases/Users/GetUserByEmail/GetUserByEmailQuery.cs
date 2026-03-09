namespace ContrateJa.Application.UseCases.Users.GetUserByEmail;

public sealed class GetUserByEmailQuery(string email)
{
    public string Email { get; } = email;
}