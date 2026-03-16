using ContrateJa.Application.UseCases.Users.Shared;
using MediatR;

namespace ContrateJa.Application.UseCases.Users.GetUserById;

public sealed class GetUserByIdQuery(long userId) : IRequest<UserResponse>
{
  public long UserId { get; } = userId > 0
    ? userId
    : throw new ArgumentOutOfRangeException(nameof(userId));
}