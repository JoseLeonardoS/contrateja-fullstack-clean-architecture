using ContrateJa.Domain.Enums;
using ContrateJa.Domain.Primitives;
using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Domain.Entities;

public sealed class User : Entity
{
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
        if (name is null)
            throw new ArgumentNullException(nameof(name));

        if (phone is null)
            throw new ArgumentNullException(nameof(phone));

        if (email is null)
            throw new ArgumentNullException(nameof(email));

        if (passwordHash is null)
            throw new ArgumentNullException(nameof(passwordHash));

        if (document is null)
            throw new ArgumentNullException(nameof(document));

        if (state is null)
            throw new ArgumentNullException(nameof(state));

        if (city is null)
            throw new ArgumentNullException(nameof(city));

        if (street is null)
            throw new ArgumentNullException(nameof(street));

        if (zipCode is null)
            throw new ArgumentNullException(nameof(zipCode));

        if (isAvailable && !AllowsAvailability(accountType))
            isAvailable = false;

        return new User(name, phone, email, passwordHash, accountType, isAvailable, document, state, city, street, zipCode);
    }

    public void ChangeName(Name newName)
    {
        if (newName is null)
            throw new ArgumentNullException(nameof(newName));

        if (Equals(Name, newName))
            return;

        Name = newName;
        Touch();
    }

    public void ChangePhone(Phone newPhone)
    {
        if (newPhone is null)
            throw new ArgumentNullException(nameof(newPhone));

        if (Equals(Phone, newPhone))
            return;

        Phone = newPhone;
        Touch();
    }

    public void ChangeEmail(Email newEmail)
    {
        if (newEmail is null)
            throw new ArgumentNullException(nameof(newEmail));

        if (Equals(Email, newEmail))
            return;

        Email = newEmail;
        Touch();
    }

    public void ChangePassword(PasswordHash newPasswordHash)
    {
        if (newPasswordHash is null)
            throw new ArgumentNullException(nameof(newPasswordHash));

        PasswordHash = newPasswordHash;
        Touch();
    }

    public void ChangeAddress(State newState, City newCity, Street newStreet, ZipCode newZipCode)
    {
        if (newState is null)
            throw new ArgumentNullException(nameof(newState));

        if (newCity is null)
            throw new ArgumentNullException(nameof(newCity));

        if (newStreet is null)
            throw new ArgumentNullException(nameof(newStreet));

        if (newZipCode is null)
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
        => accountType is EAccountType.Freelancer or EAccountType.Both;
}