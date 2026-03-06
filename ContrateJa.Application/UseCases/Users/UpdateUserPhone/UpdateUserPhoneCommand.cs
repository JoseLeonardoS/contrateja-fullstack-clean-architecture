using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Application.UseCases.Users.UpdateUserPhone;

public sealed class UpdateUserPhoneCommand
{
  public long UserId { get; }
  public Phone NewPhone { get; }

  public UpdateUserPhoneCommand(long userId, Phone newPhone)
  {
    UserId = userId;
    NewPhone = newPhone;
  }
}