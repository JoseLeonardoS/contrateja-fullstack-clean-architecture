using ContrateJa.Application.Abstractions;
using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Exceptions;
using ContrateJa.Domain.ValueObjects;
using FluentValidation;

namespace ContrateJa.Application.UseCases.Users.UpdateUserName;

public sealed class UpdateUserNameHandler : ICommandHandler<UpdateUserNameCommand>
{
  private readonly IUserRepository _userRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IValidator<UpdateUserNameCommand> _validator;

  public UpdateUserNameHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IValidator<UpdateUserNameCommand> validator)
  {
    _userRepository = userRepository;
    _unitOfWork = unitOfWork;
    _validator = validator;
  }

  public async Task Execute(UpdateUserNameCommand command, CancellationToken ct = default)
  {
    var result = await _validator.ValidateAsync(command, ct);
    if (!result.IsValid)
      throw new ValidationException(result.Errors);
      
    var user = await _userRepository.GetById(command.UserId, ct);

    if (user is null)
      throw new NotFoundException(nameof(User), command.UserId);

    user.ChangeName(new Name(command.FirstName, command.LastName));

    await _unitOfWork.SaveChanges(ct);
  }
}