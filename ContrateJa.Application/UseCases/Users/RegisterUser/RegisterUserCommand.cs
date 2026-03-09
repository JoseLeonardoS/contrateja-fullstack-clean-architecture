namespace ContrateJa.Application.UseCases.Users.RegisterUser;

public sealed class RegisterUserCommand
{

  public string FirstName { get; }
  public string LastName { get; }
  public string Phone { get; }
  public string CountryCode { get; }
  public string Email { get; }
  public string Password { get; }
  public string AccountType { get; }
  public bool IsAvailable { get; }
  public string Document { get; }
  public string State { get; }
  public string City { get; }
  public string Street { get; }
  public string ZipCode { get; }


  public RegisterUserCommand(
    string firstName,
    string lastName,
    string phone,
    string countryCode,
    string email,
    string password,
    string accountType,
    bool isAvailable,
    string document,
    string state,
    string city,
    string street,
    string zipCode)
  {
    FirstName = firstName;
    LastName = lastName;
    Phone = phone;
    CountryCode = countryCode;
    Email = email;
    Password = password;
    AccountType = accountType;
    IsAvailable = isAvailable;
    Document = document;
    State = state;
    City = city;
    Street = street;
    ZipCode = zipCode;
  }
}