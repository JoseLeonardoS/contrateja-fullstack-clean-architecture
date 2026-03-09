using ContrateJa.Application.Abstractions;
using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.Abstractions.Services;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Exceptions;
using ContrateJa.Domain.ValueObjects;
using FluentValidation;

namespace ContrateJa.Application.UseCases.Users.AuthenticateUser;

public sealed class AuthenticateUserHandler : ICommandHandler<AuthenticateUserCommand, AuthenticateUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<AuthenticateUserCommand> _validator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenGenerator _tokenGenerator;

    public AuthenticateUserHandler(
        IUserRepository userRepository,
        IValidator<AuthenticateUserCommand> validator, 
        IPasswordHasher passwordHasher, 
        ITokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _validator = validator;
        _passwordHasher = passwordHasher;
        _tokenGenerator = tokenGenerator;
    }
    
    public async Task<AuthenticateUserResponse> Execute(AuthenticateUserCommand command, CancellationToken ct = default)
    {
        var result = await _validator.ValidateAsync(command, ct);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);
        
        var user = await _userRepository.GetByEmail(new Email(command.Email), ct);
        if(user is null)
            throw new NotFoundException(nameof(User), command.Email);
        
        if(!_passwordHasher.Validate(command.Password, user.PasswordHash.Hash))
            throw new UnauthorizedException("Invalid password");
        
        var token = _tokenGenerator.GenerateToken(user.Id, user.Email.Address, user.AccountType.ToString());

        return new AuthenticateUserResponse(token);
    }
}
