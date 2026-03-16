using ContrateJa.Application.UseCases.Users.Shared;
using MediatR;

namespace ContrateJa.Application.UseCases.Users.GetUserByEmail;

public sealed class GetUserByEmailQuery(string email) : IRequest<UserResponse>
{
    public string Email { get; } = email;
}