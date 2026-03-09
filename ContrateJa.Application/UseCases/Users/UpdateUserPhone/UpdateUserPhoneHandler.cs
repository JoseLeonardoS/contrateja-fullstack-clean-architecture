using ContrateJa.Application.Abstractions;
using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Enums;
using ContrateJa.Domain.Exceptions;
using ContrateJa.Domain.ValueObjects;
using FluentValidation;

namespace ContrateJa.Application.UseCases.Users.UpdateUserPhone;

public sealed class UpdateUserPhoneHandler : ICommandHandler<UpdateUserPhoneCommand>
{
  private readonly IUserRepository _userRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IValidator<UpdateUserPhoneCommand> _validator;

  public UpdateUserPhoneHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IValidator<UpdateUserPhoneCommand> validator)
  {
    _userRepository = userRepository;
    _unitOfWork = unitOfWork;
    _validator = validator;
  }

  public async Task Execute(UpdateUserPhoneCommand command, CancellationToken ct = default)
  {
    var result = await _validator.ValidateAsync(command, ct);
    if (!result.IsValid)
      throw new ValidationException(result.Errors);

    var user = await _userRepository.GetById(command.UserId, ct);
    if (user is null)
      throw new NotFoundException(nameof(User), command.UserId);
    
    var countryCode = Enum.Parse<ECountryCode>(command.CountryCode);

    user.ChangePhone(new Phone(countryCode, command.Phone));

    await _unitOfWork.SaveChanges(ct);
  }
}