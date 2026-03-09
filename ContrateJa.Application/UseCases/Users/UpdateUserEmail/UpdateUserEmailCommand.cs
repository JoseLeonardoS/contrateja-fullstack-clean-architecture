namespace ContrateJa.Application.UseCases.Users.UpdateUserEmail;

public sealed class UpdateUserEmailCommand
{
  public long UserId { get; }
  public string NewEmail { get; }

  public UpdateUserEmailCommand(long userId, string newEmail)
  {
    UserId = userId > 0 ? userId : throw new ArgumentOutOfRangeException(nameof(userId));
    NewEmail = newEmail;
  }
}