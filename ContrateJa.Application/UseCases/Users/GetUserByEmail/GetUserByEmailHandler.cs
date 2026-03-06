using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.DTOs;

namespace ContrateJa.Application.UseCases.Users.GetUserByEmail;

public sealed class GetUserByEmailHandler
{
  private readonly IUserRepository _userRepository;

  public GetUserByEmailHandler(IUserRepository userRepository)
    => _userRepository = userRepository;

  public async Task<UserDto> Execute(GetUserByEmailQuery query, CancellationToken ct = default)
  {
    if (query is null)
      throw new ArgumentNullException(nameof(query));

    if (query.Email is null)
      throw new ArgumentNullException(nameof(query.Email));

    var user = await _userRepository.GetByEmail(query.Email, ct);

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