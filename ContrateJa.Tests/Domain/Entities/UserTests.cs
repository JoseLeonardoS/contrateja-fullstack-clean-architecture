using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Enums;
using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Tests.Domain.Entities
{
  public sealed class UserTests
  {
    private static User CreateUser(
      EAccountType accountType = EAccountType.Freelancer,
      bool isAvailable = false,
      Name? name = null,
      Phone? phone = null,
      Email? email = null,
      PasswordHash? passwordHash = null,
      Document? document = null,
      State? state = null,
      City? city = null,
      Street? street = null,
      ZipCode? zipCode = null)
    {
      name ??= new Name("Fulano", "de Tal");
      phone ??= new Phone(ECountryCode.BR, "81987654321");
      email ??= new Email("fulano.tal@example.test");
      passwordHash ??= PasswordHash.Create("Aa1!aaaa");
      document ??= new Document("52998224725");
      state ??= new State("SP");
      city ??= new City("São Paulo");
      street ??= new Street("Av. Exemplo");
      zipCode ??= new ZipCode("01001000");

      return User.Create(
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

    public static IEnumerable<object[]> NullCreateArgs()
    {
      yield return new object[] { "name" };
      yield return new object[] { "phone" };
      yield return new object[] { "email" };
      yield return new object[] { "passwordHash" };
      yield return new object[] { "document" };
      yield return new object[] { "state" };
      yield return new object[] { "city" };
      yield return new object[] { "street" };
      yield return new object[] { "zipCode" };
    }

    [Fact]
    public void Create_SetsTimestamps()
    {
      var before = DateTime.UtcNow;
      var user = CreateUser();
      var after = DateTime.UtcNow;

      Assert.InRange(user.CreatedAt, before, after);
      Assert.InRange(user.UpdatedAt, before, after);
      Assert.True(user.UpdatedAt >= user.CreatedAt);
    }

    [Theory]
    [MemberData(nameof(NullCreateArgs))]
    public void Create_WithNullArgument_ThrowsArgumentNullException(string paramName)
    {
      var name = new Name("Fulano", "de Tal");
      var phone = new Phone(ECountryCode.BR, "81987654321");
      var email = new Email("fulano.tal@example.test");
      var passwordHash = PasswordHash.Create("Aa1!aaaa");
      var document = new Document("52998224725");
      var state = new State("SP");
      var city = new City("São Paulo");
      var street = new Street("Av. Exemplo");
      var zipCode = new ZipCode("01001000");

      Action act = paramName switch
      {
        "name" => () => User.Create(null!, phone, email, passwordHash, EAccountType.Freelancer, false, document, state, city, street, zipCode),
        "phone" => () => User.Create(name, null!, email, passwordHash, EAccountType.Freelancer, false, document, state, city, street, zipCode),
        "email" => () => User.Create(name, phone, null!, passwordHash, EAccountType.Freelancer, false, document, state, city, street, zipCode),
        "passwordHash" => () => User.Create(name, phone, email, null!, EAccountType.Freelancer, false, document, state, city, street, zipCode),
        "document" => () => User.Create(name, phone, email, passwordHash, EAccountType.Freelancer, false, null!, state, city, street, zipCode),
        "state" => () => User.Create(name, phone, email, passwordHash, EAccountType.Freelancer, false, document, null!, city, street, zipCode),
        "city" => () => User.Create(name, phone, email, passwordHash, EAccountType.Freelancer, false, document, state, null!, street, zipCode),
        "street" => () => User.Create(name, phone, email, passwordHash, EAccountType.Freelancer, false, document, state, city, null!, zipCode),
        "zipCode" => () => User.Create(name, phone, email, passwordHash, EAccountType.Freelancer, false, document, state, city, street, null!),
        _ => throw new InvalidOperationException("Invalid test case.")
      };

      var ex = Assert.Throws<ArgumentNullException>(act);
      Assert.Equal(paramName, ex.ParamName);
    }

    public static IEnumerable<object[]> AccountTypesThatDoNotAllowAvailability()
    {
      yield return new object[] { EAccountType.Contractor };
      yield return new object[] { EAccountType.Admin };
    }

    [Theory]
    [MemberData(nameof(AccountTypesThatDoNotAllowAvailability))]
    public void Create_WhenAccountTypeDoesNotAllowAvailability_ForcesIsAvailableFalse(EAccountType accountType)
    {
      var user = CreateUser(accountType: accountType, isAvailable: true);

      Assert.False(user.IsAvailable);
      Assert.Equal(accountType, user.AccountType);
    }

    [Fact]
    public void ChangeName_WithNull_Throws()
    {
      var user = CreateUser();
      Assert.Throws<ArgumentNullException>(() => user.ChangeName(null!));
    }

    [Fact]
    public void ChangeName_WhenDifferent_UpdatesNameAndUpdatedAt()
    {
      var user = CreateUser();
      var oldUpdatedAt = user.UpdatedAt;

      var newName = new Name("Beltrano", "da Silva");
      user.ChangeName(newName);

      Assert.Equal(newName, user.Name);
      Assert.True(user.UpdatedAt > oldUpdatedAt);
    }

    [Fact]
    public void ChangeName_WhenSame_DoesNotUpdateUpdatedAt()
    {
      var user = CreateUser();
      var oldUpdatedAt = user.UpdatedAt;

      user.ChangeName(user.Name);

      Assert.Equal(oldUpdatedAt, user.UpdatedAt);
    }

    [Fact]
    public void ChangePhone_WithNull_Throws()
    {
      var user = CreateUser();
      Assert.Throws<ArgumentNullException>(() => user.ChangePhone(null!));
    }

    [Fact]
    public void ChangePhone_WhenDifferent_UpdatesPhoneAndUpdatedAt()
    {
      var user = CreateUser();
      var oldUpdatedAt = user.UpdatedAt;

      var newPhone = new Phone(ECountryCode.BR, "81912345678");
      user.ChangePhone(newPhone);

      Assert.Equal(newPhone, user.Phone);
      Assert.True(user.UpdatedAt > oldUpdatedAt);
    }

    [Fact]
    public void ChangeEmail_WithNull_Throws()
    {
      var user = CreateUser();
      Assert.Throws<ArgumentNullException>(() => user.ChangeEmail(null!));
    }

    [Fact]
    public void ChangeEmail_WhenDifferent_UpdatesEmailAndUpdatedAt()
    {
      var user = CreateUser();
      var oldUpdatedAt = user.UpdatedAt;

      var newEmail = new Email("beltrano.silva@example.test");
      user.ChangeEmail(newEmail);

      Assert.Equal(newEmail, user.Email);
      Assert.True(user.UpdatedAt > oldUpdatedAt);
    }

    [Fact]
    public void ChangePassword_WithNull_Throws()
    {
      var user = CreateUser();
      Assert.Throws<ArgumentNullException>(() => user.ChangePassword(null!));
    }

    [Fact]
    public void ChangePassword_WhenDifferent_UpdatesPasswordHashAndUpdatedAt()
    {
      var user = CreateUser();
      var oldUpdatedAt = user.UpdatedAt;

      var newHash = PasswordHash.Create("Bb2@bbbb");
      user.ChangePassword(newHash);

      Assert.Equal(newHash, user.PasswordHash);
      Assert.True(user.UpdatedAt > oldUpdatedAt);
    }

    [Fact]
    public void ChangeAddress_WithNullState_Throws()
    {
      var user = CreateUser();
      Assert.Throws<ArgumentNullException>(() => user.ChangeAddress(null!, new City("São Paulo"), new Street("Av. Exemplo"), new ZipCode("01001000")));
    }

    [Fact]
    public void ChangeAddress_WhenDifferent_UpdatesAddressAndUpdatedAt()
    {
      var user = CreateUser();
      var oldUpdatedAt = user.UpdatedAt;

      var newState = new State("MG");
      var newCity = new City("Belo Horizonte");
      var newStreet = new Street("Rua do Teste");
      var newZip = new ZipCode("30140071");

      user.ChangeAddress(newState, newCity, newStreet, newZip);

      Assert.Equal(newState, user.State);
      Assert.Equal(newCity, user.City);
      Assert.Equal(newStreet, user.Street);
      Assert.Equal(newZip, user.ZipCode);
      Assert.True(user.UpdatedAt > oldUpdatedAt);
    }

    public static IEnumerable<object[]> AvailabilityAllowedAccountTypes()
    {
      yield return new object[] { EAccountType.Freelancer };
      yield return new object[] { EAccountType.Both };
    }

    [Theory]
    [MemberData(nameof(AvailabilityAllowedAccountTypes))]
    public void ChangeAvailability_ToTrue_WhenAllowed_SetsIsAvailableTrue(EAccountType accountType)
    {
      var user = CreateUser(accountType: accountType, isAvailable: false);
      var oldUpdatedAt = user.UpdatedAt;

      user.ChangeAvailability(true);

      Assert.True(user.IsAvailable);
      Assert.True(user.UpdatedAt > oldUpdatedAt);
    }

    [Theory]
    [MemberData(nameof(AccountTypesThatDoNotAllowAvailability))]
    public void ChangeAvailability_ToTrue_WhenNotAllowed_Throws(EAccountType accountType)
    {
      var user = CreateUser(accountType: accountType, isAvailable: false);

      Assert.Throws<InvalidOperationException>(() => user.ChangeAvailability(true));
    }

    [Fact]
    public void ChangeAccountType_ToNotAllowed_ForcesIsAvailableFalse()
    {
      var user = CreateUser(accountType: EAccountType.Freelancer, isAvailable: true);
      var oldUpdatedAt = user.UpdatedAt;

      user.ChangeAccountType(EAccountType.Contractor);

      Assert.Equal(EAccountType.Contractor, user.AccountType);
      Assert.False(user.IsAvailable);
      Assert.True(user.UpdatedAt > oldUpdatedAt);
    }

    [Fact]
    public void ChangeAccountType_WhenSame_DoesNotUpdateUpdatedAt()
    {
      var user = CreateUser();
      var oldUpdatedAt = user.UpdatedAt;

      user.ChangeAccountType(user.AccountType);

      Assert.Equal(oldUpdatedAt, user.UpdatedAt);
    }
  }
}