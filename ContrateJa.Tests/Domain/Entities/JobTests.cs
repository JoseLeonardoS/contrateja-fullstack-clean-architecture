using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Enums;
using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Tests.Domain.Entities
{
  public sealed class JobTests
  {
    private static Job CreateJob(
      long contractorId = 1,
      string? title = null,
      string? description = null,
      State? state = null,
      City? city = null,
      Street? street = null,
      ZipCode? zipCode = null)
    {
      title ??= "Plumbing service";
      description ??= "Fix leaking faucet.";
      state ??= new State("SP");
      city ??= new City("São Paulo");
      street ??= new Street("Av. Exemplo");
      zipCode ??= new ZipCode("01001000");

      return Job.Create(contractorId, title, description, state, city, street, zipCode);
    }

    [Fact]
    public void Create_SetsDefaultStatusToOpen()
    {
      var job = CreateJob();
      Assert.Equal(EJobStatus.Open, job.Status);
    }

    [Fact]
    public void Create_SetsTimestamps()
    {
      var before = DateTime.UtcNow;
      var job = CreateJob();
      var after = DateTime.UtcNow;

      Assert.InRange(job.CreatedAt, before, after);
      Assert.InRange(job.UpdatedAt, before, after);
      Assert.True(job.UpdatedAt >= job.CreatedAt);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Create_WithInvalidContractorId_Throws(long contractorId)
    {
      var ex = Assert.Throws<ArgumentException>(() => CreateJob(contractorId: contractorId));
      Assert.Equal("contractorId", ex.ParamName);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithInvalidTitle_Throws(string? title)
    {
      var ex = Assert.Throws<ArgumentException>(() => CreateJob(title: title));
      Assert.Equal("title", ex.ParamName);
    }

    [Fact]
    public void Create_WithTooLongTitle_Throws()
    {
      var title = new string('a', 151);
      var ex = Assert.Throws<ArgumentException>(() => CreateJob(title: title));
      Assert.Equal("title", ex.ParamName);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithInvalidDescription_Throws(string? description)
    {
      var ex = Assert.Throws<ArgumentException>(() => CreateJob(description: description));
      Assert.Equal("description", ex.ParamName);
    }

    [Fact]
    public void Create_WithTooLongDescription_Throws()
    {
      var description = new string('a', 1001);
      var ex = Assert.Throws<ArgumentException>(() => CreateJob(description: description));
      Assert.Equal("description", ex.ParamName);
    }

    public static IEnumerable<object[]> NullAddressArgs()
    {
      yield return new object[] { "state" };
      yield return new object[] { "city" };
      yield return new object[] { "street" };
      yield return new object[] { "zipCode" };
    }

    [Theory]
    [MemberData(nameof(NullAddressArgs))]
    public void Create_WithNullAddressPart_ThrowsArgumentNullException(string paramName)
    {
      var state = new State("SP");
      var city = new City("São Paulo");
      var street = new Street("Av. Exemplo");
      var zipCode = new ZipCode("01001000");

      Action act = paramName switch
      {
        "state" => () => Job.Create(1, "Title", "Desc", null!, city, street, zipCode),
        "city" => () => Job.Create(1, "Title", "Desc", state, null!, street, zipCode),
        "street" => () => Job.Create(1, "Title", "Desc", state, city, null!, zipCode),
        "zipCode" => () => Job.Create(1, "Title", "Desc", state, city, street, null!),
        _ => throw new InvalidOperationException("Invalid test case.")
      };

      var ex = Assert.Throws<ArgumentNullException>(act);
      Assert.Equal(paramName, ex.ParamName);
    }

    [Fact]
    public void Create_TrimsTitleAndDescription()
    {
      var job = CreateJob(title: "  My Title  ", description: "  My Desc  ");
      Assert.Equal("My Title", job.Title);
      Assert.Equal("My Desc", job.Description);
    }

    [Fact]
    public void UpdateTitle_WithNullOrWhiteSpace_Throws()
    {
      var job = CreateJob();
      Assert.Throws<ArgumentException>(() => job.UpdateTitle(" "));
    }

    [Fact]
    public void UpdateTitle_WhenDifferent_UpdatesTitleAndUpdatedAt()
    {
      var job = CreateJob();
      var oldUpdatedAt = job.UpdatedAt;

      job.UpdateTitle("New title");

      Assert.Equal("New title", job.Title);
      Assert.True(job.UpdatedAt > oldUpdatedAt);
    }

    [Fact]
    public void UpdateTitle_WhenSame_DoesNotUpdateUpdatedAt()
    {
      var job = CreateJob();
      var oldUpdatedAt = job.UpdatedAt;

      job.UpdateTitle(job.Title);

      Assert.Equal(oldUpdatedAt, job.UpdatedAt);
    }

    [Fact]
    public void UpdateTitle_TrimsInput()
    {
      var job = CreateJob();
      job.UpdateTitle("  New title  ");
      Assert.Equal("New title", job.Title);
    }

    [Fact]
    public void UpdateDescription_WithNullOrWhiteSpace_Throws()
    {
      var job = CreateJob();
      Assert.Throws<ArgumentException>(() => job.UpdateDescription(" "));
    }

    [Fact]
    public void UpdateDescription_WhenDifferent_UpdatesDescriptionAndUpdatedAt()
    {
      var job = CreateJob();
      var oldUpdatedAt = job.UpdatedAt;

      job.UpdateDescription("New description");

      Assert.Equal("New description", job.Description);
      Assert.True(job.UpdatedAt > oldUpdatedAt);
    }

    [Fact]
    public void UpdateDescription_WhenSame_DoesNotUpdateUpdatedAt()
    {
      var job = CreateJob();
      var oldUpdatedAt = job.UpdatedAt;

      job.UpdateDescription(job.Description);

      Assert.Equal(oldUpdatedAt, job.UpdatedAt);
    }

    [Fact]
    public void UpdateDescription_TrimsInput()
    {
      var job = CreateJob();
      job.UpdateDescription("  New description  ");
      Assert.Equal("New description", job.Description);
    }

    [Fact]
    public void UpdateState_WithNull_Throws()
    {
      var job = CreateJob();
      Assert.Throws<ArgumentNullException>(() => job.UpdateState(null!));
    }

    [Fact]
    public void UpdateState_WhenDifferent_UpdatesStateAndUpdatedAt()
    {
      var job = CreateJob();
      var oldUpdatedAt = job.UpdatedAt;

      var newState = new State("RJ");
      job.UpdateState(newState);

      Assert.Equal(newState, job.State);
      Assert.True(job.UpdatedAt > oldUpdatedAt);
    }

    [Fact]
    public void UpdateState_WhenSame_DoesNotUpdateUpdatedAt()
    {
      var job = CreateJob();
      var oldUpdatedAt = job.UpdatedAt;

      job.UpdateState(job.State);

      Assert.Equal(oldUpdatedAt, job.UpdatedAt);
    }

    [Fact]
    public void UpdateCity_WithNull_Throws()
    {
      var job = CreateJob();
      Assert.Throws<ArgumentNullException>(() => job.UpdateCity(null!));
    }

    [Fact]
    public void UpdateCity_WhenDifferent_UpdatesCityAndUpdatedAt()
    {
      var job = CreateJob();
      var oldUpdatedAt = job.UpdatedAt;

      var newCity = new City("Rio de Janeiro");
      job.UpdateCity(newCity);

      Assert.Equal(newCity, job.City);
      Assert.True(job.UpdatedAt > oldUpdatedAt);
    }

    [Fact]
    public void UpdateStreet_WithNull_Throws()
    {
      var job = CreateJob();
      Assert.Throws<ArgumentNullException>(() => job.UpdateStreet(null!));
    }

    [Fact]
    public void UpdateStreet_WhenDifferent_UpdatesStreetAndUpdatedAt()
    {
      var job = CreateJob();
      var oldUpdatedAt = job.UpdatedAt;

      var newStreet = new Street("Rua Teste");
      job.UpdateStreet(newStreet);

      Assert.Equal(newStreet, job.Street);
      Assert.True(job.UpdatedAt > oldUpdatedAt);
    }

    [Fact]
    public void UpdateZipCode_WithNull_Throws()
    {
      var job = CreateJob();
      Assert.Throws<ArgumentNullException>(() => job.UpdateZipCode(null!));
    }

    [Fact]
    public void UpdateZipCode_WhenDifferent_UpdatesZipCodeAndUpdatedAt()
    {
      var job = CreateJob();
      var oldUpdatedAt = job.UpdatedAt;

      var newZip = new ZipCode("30140071");
      job.UpdateZipCode(newZip);

      Assert.Equal(newZip, job.ZipCode);
      Assert.True(job.UpdatedAt > oldUpdatedAt);
    }

    [Fact]
    public void UpdateStatus_WithInvalidEnumValue_Throws()
    {
      var job = CreateJob();
      Assert.Throws<ArgumentOutOfRangeException>(() => job.UpdateStatus((EJobStatus)999));
    }

    [Fact]
    public void UpdateStatus_WhenDifferent_UpdatesStatusAndUpdatedAt()
    {
      var job = CreateJob();
      var oldUpdatedAt = job.UpdatedAt;

      job.UpdateStatus(EJobStatus.Closed);

      Assert.Equal(EJobStatus.Closed, job.Status);
      Assert.True(job.UpdatedAt > oldUpdatedAt);
    }

    [Fact]
    public void UpdateStatus_WhenSame_DoesNotUpdateUpdatedAt()
    {
      var job = CreateJob();
      var oldUpdatedAt = job.UpdatedAt;

      job.UpdateStatus(job.Status);

      Assert.Equal(oldUpdatedAt, job.UpdatedAt);
    }

    [Fact]
    public void UpdateStatus_WhenAlreadyClosed_Throws()
    {
      var job = CreateJob();
      job.UpdateStatus(EJobStatus.Closed);

      Assert.Throws<InvalidOperationException>(() => job.UpdateStatus(EJobStatus.Open));
    }
  }
}