using ContrateJa.Domain.Enums;
using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Application.UseCases.Users.RegisterUser;

public sealed class RegisterUserCommand
{

  public Name Name { get; }
  public Phone Phone { get; }
  public Email Email { get; }
  public PasswordHash PasswordHash { get; }
  public EAccountType AccountType { get; }
  public bool IsAvailable { get; }
  public Document Document { get; }
  public State State { get; }
  public City City { get; }
  public Street Street { get; }
  public ZipCode ZipCode { get; }


  public RegisterUserCommand(
    Name name,
    Phone phone,
    Email email,
    PasswordHash passwordHash,
    EAccountType accountType,
    bool isAvailable,
    Document document,
    State state,
    City city,
    Street street,
    ZipCode zipCode)
  {
    Name = name;
    Phone = phone;
    Email = email;
    PasswordHash = passwordHash;
    AccountType = accountType;
    IsAvailable = isAvailable;
    Document = document;
    State = state;
    City = city;
    Street = street;
    ZipCode = zipCode;
  }
}