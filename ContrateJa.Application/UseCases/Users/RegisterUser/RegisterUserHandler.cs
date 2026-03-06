using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;

namespace ContrateJa.Application.UseCases.Users.RegisterUser;

public sealed class RegisterUserHandler
{
  private readonly IUserRepository _userRepository;
  private readonly IUnitOfWork _unitOfWork;

  public RegisterUserHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
  {
    _userRepository = userRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task Execute(RegisterUserCommand command, CancellationToken ct = default)
  {
    if (command is null)
      throw new ArgumentNullException(nameof(command));

    if (await _userRepository.ExistsByEmail(command.Email, ct))
      throw new InvalidOperationException("Email already exists.");

    // TODO: Add rules to encrypt the password

    var user = User.Create(
      command.Name,
      command.Phone,
      command.Email,
      command.PasswordHash,
      command.AccountType,
      command.IsAvailable,
      command.Document,
      command.State,
      command.City,
      command.Street,
      command.ZipCode);

    await _userRepository.Add(user, ct);
    await _unitOfWork.SaveChanges(ct);
  }
}