namespace ContrateJa.Application.UseCases.Users.DeleteUser;

public sealed class DeleteUserCommand
{
  public long UserId { get; }

  public DeleteUserCommand(long userId)
    => UserId = userId;
}