using ContrateJa.Application.Abstractions.Repositories;

namespace ContrateJa.Application.UseCases.Users.UpdateUserPhone;

public sealed class UpdateUserPhoneHandler
{
  private readonly IUserRepository _userRepository;
  private readonly IUnitOfWork _unitOfWork;

  public UpdateUserPhoneHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
  {
    _userRepository = userRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task Execute(UpdateUserPhoneCommand command, CancellationToken ct = default)
  {
    if (command is null)
      throw new ArgumentNullException(nameof(command));

    if (command.UserId <= 0)
      throw new ArgumentOutOfRangeException(nameof(command.UserId));

    var user = await _userRepository.GetById(command.UserId, ct);

    if (user is null)
      throw new InvalidOperationException("User not found.");

    user.ChangePhone(command.NewPhone);

    await _unitOfWork.SaveChanges(ct);
  }
}