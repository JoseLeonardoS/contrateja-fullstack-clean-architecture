using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Application.UseCases.Users.UpdateUserName;

public sealed class UpdateUserNameCommand
{
  public long UserId { get; }
  public Name NewName { get; }

  public UpdateUserNameCommand(long userId, Name newName)
  {
    UserId = userId;
    NewName = newName;
  }
}