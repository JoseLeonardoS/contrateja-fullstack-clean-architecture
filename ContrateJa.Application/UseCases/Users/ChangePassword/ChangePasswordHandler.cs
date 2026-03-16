using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.Abstractions.Services;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Exceptions;
using ContrateJa.Domain.ValueObjects;
using FluentValidation;
using MediatR;

namespace ContrateJa.Application.UseCases.Users.ChangePassword;

public sealed class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<ChangePasswordCommand> _validator;
    private readonly IPasswordHasher _passwordHasher;
    
    public ChangePasswordHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IValidator<ChangePasswordCommand> validator,  IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _passwordHasher = passwordHasher;
    }
    
    public async Task Handle(ChangePasswordCommand command, CancellationToken ct = default)
    {
        var result = await _validator.ValidateAsync(command, ct);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);
        
        var user = await  _userRepository.GetById(command.UserId, ct);
        if (user is null)
            throw new NotFoundException(nameof(User), command.UserId);
        
        if(command.OldPassword == command.NewPassword)
            throw new ArgumentException("The both  old and new passwords are equal.");
        
        var validPass = _passwordHasher.Validate(command.OldPassword, user.PasswordHash.Hash);
        if (!validPass)
            throw new UnauthorizedException("This password is incorrect.");
        
        var newHash = _passwordHasher.Hash(new Password(command.NewPassword).Value);
        
        user.ChangePassword(new PasswordHash(newHash));

        await _unitOfWork.SaveChanges(ct);
    }
}