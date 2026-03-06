namespace ContrateJa.Application.UseCases.Users.GetUserById;

public sealed class GetUserByIdQuery
{
  public long UserId { get; }

  public GetUserByIdQuery(long userId)
    => UserId = userId;
}