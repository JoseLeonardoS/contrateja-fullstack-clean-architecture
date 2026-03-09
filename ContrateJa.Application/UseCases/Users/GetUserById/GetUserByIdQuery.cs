namespace ContrateJa.Application.UseCases.Users.GetUserById;

public sealed class GetUserByIdQuery(long userId)
{
  public long UserId { get; } = userId > 0
    ? userId
    : throw new ArgumentOutOfRangeException(nameof(userId));
}