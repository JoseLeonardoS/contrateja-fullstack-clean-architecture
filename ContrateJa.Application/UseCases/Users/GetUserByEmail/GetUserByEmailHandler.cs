using ContrateJa.Application.Abstractions;
using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.UseCases.Users.Shared;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Exceptions;
using ContrateJa.Domain.ValueObjects;
using FluentValidation;

namespace ContrateJa.Application.UseCases.Users.GetUserByEmail;

public sealed class GetUserByEmailHandler : IQueryHandler<GetUserByEmailQuery, UserResponse>
{
  private readonly IUserRepository _userRepository;
  private readonly IValidator<GetUserByEmailQuery> _validator;

  public GetUserByEmailHandler(
    IUserRepository userRepository,
    IValidator<GetUserByEmailQuery> validator)
  {
    _userRepository = userRepository;
    _validator = validator;
  }

  public async Task<UserResponse> Execute(GetUserByEmailQuery query, CancellationToken ct = default)
  {
    var result = await  _validator.ValidateAsync(query, ct);
    
    if(!result.IsValid)
      throw new ValidationException(result.Errors);

    var user = await _userRepository.GetByEmail(new Email(query.Email), ct);

    if (user is null)
      throw new NotFoundException(nameof(User), query.Email);

    return new UserResponse(
      user.Id,
      user.Name.FirstName,
      user.Name.LastName,
      user.Email.Address,
      user.AccountType.ToString(),
      user.IsAvailable,
      user.State.Code,
      user.City.Name,
      user.CreatedAt);
  }
}