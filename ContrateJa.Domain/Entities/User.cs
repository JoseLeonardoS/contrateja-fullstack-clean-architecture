using ContrateJa.Domain.Enums;
using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Domain.Entities
{
  public sealed class User
  {
    public long Id { get; private set; }
    public Name Name { get; private set; }
    public Phone Phone { get; private set; }
    public Email Email { get; private set; }
    public PasswordHash PasswordHash { get; private set; }
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
      CreatedAt = DateTime.UtcNow;
      UpdatedAt = DateTime.UtcNow;
    }

    public static User Create(
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
      if (name == null)
        throw new ArgumentNullException(nameof(name));

      if (phone == null)
        throw new ArgumentNullException(nameof(phone));

      if (email == null)
        throw new ArgumentNullException(nameof(email));

      if (passwordHash == null)
        throw new ArgumentNullException(nameof(passwordHash));

      if (document == null)
        throw new ArgumentNullException(nameof(document));

      if (state == null)
        throw new ArgumentNullException(nameof(state));

      if (city == null)
        throw new ArgumentNullException(nameof(city));

      if (street == null)
        throw new ArgumentNullException(nameof(street));

      if (zipCode == null)
        throw new ArgumentNullException(nameof(zipCode));

      if (isAvailable && !AllowsAvailability(accountType))
        isAvailable = false;

      return new User(
        name,
        phone,
        email,
        passwordHash,
        accountType,
        isAvailable,
        document,
        state,
        city,
        street,
        zipCode);
    }

    public void ChangeName(Name newName)
    {
      if (newName == null)
        throw new ArgumentNullException(nameof(newName));

      if (Equals(Name, newName))
        return;

      Name = newName;
      Touch();
    }

    public void ChangePhone(Phone newPhone)
    {
      if (newPhone == null)
        throw new ArgumentNullException(nameof(newPhone));

      if (Equals(Phone, newPhone))
        return;

      Phone = newPhone;
      Touch();
    }

    public void ChangeEmail(Email newEmail)
    {
      if (newEmail == null)
        throw new ArgumentNullException(nameof(newEmail));

      if (Equals(Email, newEmail))
        return;

      Email = newEmail;
      Touch();
    }

    public void ChangePassword(PasswordHash newHash)
    {
      if (newHash == null)
        throw new ArgumentNullException(nameof(newHash));

      if (Equals(PasswordHash, newHash))
        return;

      PasswordHash = newHash;
      Touch();
    }

    public void ChangeAddress(State newState, City newCity, Street newStreet, ZipCode newZipCode)
    {
      if (newState == null)
        throw new ArgumentNullException(nameof(newState));

      if (newCity == null)
        throw new ArgumentNullException(nameof(newCity));

      if (newStreet == null)
        throw new ArgumentNullException(nameof(newStreet));

      if (newZipCode == null)
        throw new ArgumentNullException(nameof(newZipCode));

      if (Equals(State, newState) &&
          Equals(City, newCity) &&
          Equals(Street, newStreet) &&
          Equals(ZipCode, newZipCode))
        return;

      State = newState;
      City = newCity;
      Street = newStreet;
      ZipCode = newZipCode;
      Touch();
    }

    public void ChangeAccountType(EAccountType newAccountType)
    {
      if (newAccountType == AccountType)
        return;

      AccountType = newAccountType;

      if (!AllowsAvailability(AccountType))
        IsAvailable = false;

      Touch();
    }

    public void ChangeAvailability(bool isAvailable)
    {
      if (isAvailable == IsAvailable)
        return;

      if (isAvailable && !AllowsAvailability(AccountType))
        throw new InvalidOperationException("Account type must be freelancer or both to change availability.");

      IsAvailable = isAvailable;
      Touch();
    }

    private static bool AllowsAvailability(EAccountType accountType)
    {
      return accountType == EAccountType.freelancer || accountType == EAccountType.both;
    }

    private void Touch()
    {
      UpdatedAt = DateTime.UtcNow;
    }
  }
}