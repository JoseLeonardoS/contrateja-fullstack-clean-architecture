using MediatR;

namespace ContrateJa.Application.UseCases.Users.UpdateUserName;

public sealed class UpdateUserNameCommand : IRequest
{
  public long UserId { get; }
  public string FirstName { get; }
  public string LastName { get; }

  public UpdateUserNameCommand(long userId, string firstName, string lastName)
  {
    UserId = userId > 0 ? userId : throw new ArgumentOutOfRangeException(nameof(userId));
    FirstName = firstName;
    LastName = lastName;
  }
}