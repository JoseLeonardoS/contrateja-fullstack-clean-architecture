using ContrateJa.Domain.Enums;
using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Domain.Entities
{
  public class User
  {
    public int Id { get; private set; }
    public Name Name { get; private set; }
    public Phone Phone { get; private set; }
    public Email Email { get; private set; }
    public string PasswordHash { get; private set; }
    public EAccountType AccountType { get; private set; }
    public bool IsAvailable { get; private set; }
    public Document Document { get; private set; }
    public State State { get; private set; }
    public City City { get; private set; }
    public Street Street { get; private set; }
    public ZipCode ZipCode { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private User() { }

    private User(
      Name name,
      Phone phone,
      Email email,
      string passwordHash,
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
      CreatedAt = DateTime.UtcNow;
      UpdatedAt = DateTime.UtcNow;
    }

    public static User Create(
      Name name,
      Phone phone,
      Email email,
      string passwordHash,
      EAccountType accountType,
      bool isAvailable,
      Document document,
      State state,
      City city,
      Street street,
      ZipCode zipCode)
    {
      // TODO: Add any necessary validation or business logic here
      return new User(
        name,
        phone,
        email,
        passwordHash,
        accountType,
        isAvailable,
        document,
        state, city,
        street,
        zipCode);
    }
  }
}
