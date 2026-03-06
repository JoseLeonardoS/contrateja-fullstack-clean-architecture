using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Application.UseCases.Users.GetUserByEmail;

public sealed class GetUserByEmailQuery
{
  public Email Email { get; }

  public GetUserByEmailQuery(Email email)
    => Email = email;
}