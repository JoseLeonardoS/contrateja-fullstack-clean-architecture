using ContrateJa.Application.Abstractions;
using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Enums;
using ContrateJa.Domain.Exceptions;

namespace ContrateJa.Application.UseCases.Users.ChangeAccountType;

public sealed class ChangeAccountTypeHandler : ICommandHandler<ChangeAccountTypeCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeAccountTypeHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Execute(ChangeAccountTypeCommand command, CancellationToken ct = default)
    {
        var user = await _userRepository.GetById(command.UserId, ct);
        if (user is null)
            throw new NotFoundException(nameof(User),  command.UserId);
        
        var accountType = Enum.Parse<EAccountType>(command.NewAccountType);
        
        user.ChangeAccountType(accountType);
        
        await _unitOfWork.SaveChanges(ct);
    }
}