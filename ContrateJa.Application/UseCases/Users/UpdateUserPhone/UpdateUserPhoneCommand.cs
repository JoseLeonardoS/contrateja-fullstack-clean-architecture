using MediatR;

namespace ContrateJa.Application.UseCases.Users.UpdateUserPhone;

public sealed class UpdateUserPhoneCommand : IRequest
{
  public long UserId { get; }
  public string Phone { get; }
  public string CountryCode { get; }

  public UpdateUserPhoneCommand(long userId, string phone, string countryCode)
  {
    UserId = userId > 0 ? userId : throw new ArgumentOutOfRangeException(nameof(userId));
    Phone = phone;
    CountryCode = countryCode;
  }
}