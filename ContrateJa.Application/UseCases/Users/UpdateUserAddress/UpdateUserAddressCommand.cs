using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Application.UseCases.Users.UpdateUserAddress;

public sealed class UpdateUserAddressCommand
{
  public long UserId { get; }
  public State? NewState { get; }
  public City? NewCity { get; }
  public Street? NewStreet { get; }
  public ZipCode? NewZipCode { get; }

  public UpdateUserAddressCommand(
    long userId,
    State? newState = null,
    City? newCity = null,
    Street? newStreet = null,
    ZipCode? newZipCode = null)
  {
    UserId = userId;
    NewState = newState;
    NewCity = newCity;
    NewStreet = newStreet;
    NewZipCode = newZipCode;
  }
}