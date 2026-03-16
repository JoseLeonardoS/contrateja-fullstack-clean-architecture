using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.Abstractions.Services;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Enums;
using ContrateJa.Domain.Exceptions;
using ContrateJa.Domain.ValueObjects;
using FluentValidation;
using MediatR;

namespace ContrateJa.Application.UseCases.Users.RegisterUser;

public sealed class RegisterUserHandler : IRequestHandler<RegisterUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<RegisterUserCommand> _validator;
    private readonly IPasswordHasher _passwordHasher;
    
    public RegisterUserHandler(
        IUserRepository userRepository, 
        IUnitOfWork unitOfWork, 
        IValidator<RegisterUserCommand> validator,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _passwordHasher = passwordHasher;
    }
    
    public async Task Handle(RegisterUserCommand command, CancellationToken ct = default)
    {
        var result = await _validator.ValidateAsync(command, ct);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);
        
        var emailExists = await _userRepository.ExistsByEmail(new Email(command.Email), ct);
        if(emailExists) throw new ConflictException("Email address already exists.");
        
        var name = new Name(command.FirstName, command.LastName);
        var password = new Password(command.Password);
        var passwordHash = _passwordHasher.Hash(password.Value);
        var countryCode = Enum.Parse<ECountryCode>(command.CountryCode);
        var accountType = Enum.Parse<EAccountType>(command.AccountType);

        var user = User.Create(
            name,
            new Phone(countryCode, command.Phone),
            new Email(command.Email), 
            new PasswordHash(passwordHash),
            accountType,
            accountType != EAccountType.Contractor,
            new Document(command.Document),
            new State(command.State),
            new City(command.City),
            new Street(command.Street),
            new ZipCode(command.ZipCode)); 

        await _userRepository.Add(user, ct);
        await _unitOfWork.SaveChanges(ct);
    }
}
