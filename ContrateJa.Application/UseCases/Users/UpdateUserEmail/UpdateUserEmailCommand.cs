using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Application.UseCases.Users.UpdateUserEmail;

public sealed class UpdateUserEmailCommand
{
  public long UserId { get; }
  public Email NewEmail { get; }

  public UpdateUserEmailCommand(long userId, Email newEmail)
  {
    UserId = userId;
    NewEmail = newEmail;
  }
}