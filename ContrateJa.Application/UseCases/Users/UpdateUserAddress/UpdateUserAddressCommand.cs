namespace ContrateJa.Application.UseCases.Users.UpdateUserAddress;

public sealed class UpdateUserAddressCommand
{
  public long UserId { get; }
  public string NewState { get; }
  public string NewCity { get; }
  public string NewStreet { get; }
  public string NewZipCode { get; }

  public UpdateUserAddressCommand(
    long userId,
    string newState,
    string newCity,
    string newStreet,
    string newZipCode)
  {
    UserId = userId > 0 ? userId : throw new ArgumentOutOfRangeException(nameof(userId));
    NewState = newState;
    NewCity = newCity;
    NewStreet = newStreet;
    NewZipCode = newZipCode;
  }
}