using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.DTOs;

namespace ContrateJa.Application.UseCases.Users.GetUserById;

public sealed class GetUserByIdHandler
{
  private readonly IUserRepository _userRepository;

  public GetUserByIdHandler(IUserRepository userRepository)
    => _userRepository = userRepository;

  public async Task<UserDto> Execute(GetUserByIdQuery query, CancellationToken ct = default)
  {
    if (query is null)
      throw new ArgumentNullException(nameof(query));

    if (query.UserId <= 0)
      throw new ArgumentOutOfRangeException(nameof(query.UserId));

    var user = await _userRepository.GetById(query.UserId, ct);

    if (user is null)
      throw new InvalidOperationException("User not found.");

    return new UserDto(
      user.Name,
      user.Phone,
      user.Email,
      user.AccountType,
      user.IsAvailable,
      user.Document,
      user.State,
      user.City,
      user.Street,
      user.ZipCode,
      user.CreatedAt,
      user.UpdatedAt);
  }
}