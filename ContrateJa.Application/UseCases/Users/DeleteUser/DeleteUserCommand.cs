using MediatR;

namespace ContrateJa.Application.UseCases.Users.DeleteUser;

public sealed class DeleteUserCommand(long userId) : IRequest
{
  public long UserId { get; } =  userId > 0 
      ? userId 
      : throw new ArgumentOutOfRangeException(nameof(userId));
}