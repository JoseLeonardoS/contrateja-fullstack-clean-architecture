using ContrateJa.Application.Abstractions.Repositories;

namespace ContrateJa.Application.UseCases.Users.UpdateUserName;

public sealed class UpdateUserNameHandler
{
  private readonly IUserRepository _userRepository;
  private readonly IUnitOfWork _unitOfWork;

  public UpdateUserNameHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
  {
    _userRepository = userRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task Execute(UpdateUserNameCommand command, CancellationToken ct = default)
  {
    if (command is null)
      throw new ArgumentNullException(nameof(command));

    if (command.UserId <= 0)
      throw new ArgumentOutOfRangeException(nameof(command.UserId));

    var user = await _userRepository.GetById(command.UserId, ct);

    if (user is null)
      throw new InvalidOperationException("User not found.");

    user.ChangeName(command.NewName);

    await _unitOfWork.SaveChanges(ct);
  }
}