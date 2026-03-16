using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Exceptions;
using ContrateJa.Domain.ValueObjects;
using FluentValidation;
using MediatR;

namespace ContrateJa.Application.UseCases.Users.UpdateUserAddress;

public sealed class UpdateUserAddressHandler : IRequestHandler<UpdateUserAddressCommand>
{
  private readonly IUserRepository _userRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IValidator<UpdateUserAddressCommand> _validator;

  public UpdateUserAddressHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IValidator<UpdateUserAddressCommand> validator)
  {
    _userRepository = userRepository;
    _unitOfWork = unitOfWork;
    _validator = validator;
  }


  public async Task Handle(UpdateUserAddressCommand command, CancellationToken ct = default)
  {
    var result = await _validator.ValidateAsync(command, ct);
    if(!result.IsValid)
      throw new ValidationException(result.Errors);

    var user = await _userRepository.GetById(command.UserId, ct);
    if (user is null)
      throw new NotFoundException(nameof(User), command.UserId);

    user.ChangeAddress(
      new State(command.NewState),
      new City(command.NewCity),
      new Street(command.NewStreet),
      new ZipCode(command.NewZipCode));

    await _unitOfWork.SaveChanges(ct);
  }
}