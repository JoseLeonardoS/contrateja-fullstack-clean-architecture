using ContrateJa.Application.Abstractions;
using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Exceptions;
using ContrateJa.Domain.ValueObjects;
using FluentValidation;

namespace ContrateJa.Application.UseCases.Users.UpdateUserEmail;

public sealed class UpdateUserEmailHandler : ICommandHandler<UpdateUserEmailCommand>
{
  private readonly IUserRepository _userRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IValidator<UpdateUserEmailCommand> _validator;

  public UpdateUserEmailHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IValidator<UpdateUserEmailCommand> validator)
  {
    _userRepository = userRepository;
    _unitOfWork = unitOfWork;
    _validator = validator;
  }

  public async Task Execute(UpdateUserEmailCommand command, CancellationToken ct = default)
  {
    var result =  await _validator.ValidateAsync(command, ct);
    if (!result.IsValid)
      throw new ValidationException(result.Errors);

    var user = await _userRepository.GetById(command.UserId, ct);

    if (user is null)
      throw new NotFoundException(nameof(User), command.UserId);

    var newEmail = new Email(command.NewEmail);
    
    var emailExists = await _userRepository.ExistsByEmail(newEmail, ct);
    if(emailExists)
      throw new ConflictException("Email already exists.");

    user.ChangeEmail(newEmail);

    await _unitOfWork.SaveChanges(ct);
  }
}