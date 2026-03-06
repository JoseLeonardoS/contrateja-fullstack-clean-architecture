using ContrateJa.Application.Abstractions.Repositories;

namespace ContrateJa.Application.UseCases.Users.UpdateUserEmail;

public sealed class UpdateUserEmailHandler
{
  private readonly IUserRepository _userRepository;
  private readonly IUnitOfWork _unitOfWork;

  public UpdateUserEmailHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
  {
    _userRepository = userRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task Execute(UpdateUserEmailCommand command, CancellationToken ct = default)
  {
    if (command is null)
      throw new ArgumentNullException(nameof(command));

    if (command.UserId <= 0)
      throw new ArgumentOutOfRangeException(nameof(command.UserId));

    var user = await _userRepository.GetById(command.UserId, ct);

    if (user is null)
      throw new InvalidOperationException("User not found.");

    if (user.Email.Equals(command.NewEmail))
      return;

    if (await _userRepository.ExistsByEmail(command.NewEmail, ct))
      throw new InvalidOperationException("Email already in use.");

    user.ChangeEmail(command.NewEmail);

    await _unitOfWork.SaveChanges(ct);
  }
}