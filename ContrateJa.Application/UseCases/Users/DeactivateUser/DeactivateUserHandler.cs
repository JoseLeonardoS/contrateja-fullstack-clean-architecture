using ContrateJa.Application.Abstractions;
using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Exceptions;

namespace ContrateJa.Application.UseCases.Users.DeactivateUser;

public sealed class DeactivateUserHandler : ICommandHandler<DeactivateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeactivateUserHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Execute(DeactivateUserCommand command, CancellationToken ct = default)
    {
        var user  = await _userRepository.GetById(command.UserId, ct);
        if (user is null)
            throw new NotFoundException(nameof(User),  command.UserId);
        
        user.ChangeAvailability(false);

        await _unitOfWork.SaveChanges(ct);
    }
}