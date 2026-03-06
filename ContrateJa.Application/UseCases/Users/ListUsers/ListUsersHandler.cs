using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.DTOs;

namespace ContrateJa.Application.UseCases.Users.ListUsers;

public sealed class ListUsersHandler
{
  private readonly IUserRepository _userReposiroty;

  public ListUsersHandler(IUserRepository userRepository)
    => _userReposiroty = userRepository;

  public async Task<IReadOnlyList<UserDto>> Execute(CancellationToken ct = default)
  {
    var list = await _userReposiroty.ListAll(ct);

    return list.Select(u => new UserDto(
      u.Name,
      u.Phone,
      u.Email,
      u.AccountType,
      u.IsAvailable,
      u.Document,
      u.State,
      u.City,
      u.Street,
      u.ZipCode,
      u.CreatedAt,
      u.UpdatedAt))
      .ToList();
  }
}