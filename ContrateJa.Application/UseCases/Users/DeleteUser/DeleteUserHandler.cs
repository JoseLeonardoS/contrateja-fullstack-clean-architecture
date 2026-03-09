using ContrateJa.Application.Abstractions;
using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Exceptions;

namespace ContrateJa.Application.UseCases.Users.DeleteUser;

public sealed class DeleteUserHandler : ICommandHandler<DeleteUserCommand>
{
  private readonly IUserRepository _userRepository;
  private readonly IUnitOfWork _unitOfWork;

  public DeleteUserHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
  {
    _userRepository = userRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task Execute(DeleteUserCommand command, CancellationToken ct = default)
  {
    var user = await _userRepository.GetById(command.UserId, ct);

    if (user is null)
      throw new NotFoundException(nameof(User),  command.UserId);

    await _userRepository.Remove(user, ct);
    await _unitOfWork.SaveChanges(ct);
  }
}
