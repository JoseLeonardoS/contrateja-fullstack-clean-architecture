using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Exceptions;
using MediatR;

namespace ContrateJa.Application.UseCases.Users.ReactivateUser;

public sealed class ReactivateUserHandler : IRequestHandler<ReactivateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReactivateUserHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ReactivateUserCommand command, CancellationToken ct = default)
    {
        var user = await _userRepository.GetById(command.UserId, ct);
        if(user is null)
            throw new NotFoundException(nameof(User),  command.UserId);
        
        if(user.IsAvailable)
            return;
        
        user.ChangeAvailability(true);

        await _unitOfWork.SaveChanges(ct);
    }
}