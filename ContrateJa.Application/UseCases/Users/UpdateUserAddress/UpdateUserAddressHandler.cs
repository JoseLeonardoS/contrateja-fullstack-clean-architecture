using ContrateJa.Application.Abstractions.Repositories;

namespace ContrateJa.Application.UseCases.Users.UpdateUserAddress;

public sealed class UpdateUserAddressHandler
{
  private readonly IUserRepository _userRepository;
  private readonly IUnitOfWork _unitOfWork;

  public UpdateUserAddressHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
  {
    _userRepository = userRepository;
    _unitOfWork = unitOfWork;
  }


  public async Task Execute(UpdateUserAddressCommand command, CancellationToken ct = default)
  {
    if (command is null)
      throw new ArgumentNullException(nameof(command));

    if (command.UserId <= 0)
      throw new ArgumentOutOfRangeException(nameof(command.UserId));

    var user = await _userRepository.GetById(command.UserId, ct);

    if (user is null)
      throw new InvalidOperationException("User not found.");

    if (command.NewState is null &&
        command.NewCity is null &&
        command.NewStreet is null &&
        command.NewZipCode is null)
      return;

    var newState = command.NewState ?? user.State;
    var newCity = command.NewCity ?? user.City;
    var newStreet = command.NewStreet ?? user.Street;
    var newZipCode = command.NewZipCode ?? user.ZipCode;

    user.ChangeAddress(newState, newCity, newStreet, newZipCode);

    await _unitOfWork.SaveChanges(ct);
  }
}