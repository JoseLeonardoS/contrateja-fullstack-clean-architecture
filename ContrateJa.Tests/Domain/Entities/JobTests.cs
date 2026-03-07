using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Enums;
using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Tests.Domain.Entities;

public sealed class JobTests
{
    private static Job CreateJob(
        long contractorId = 1,
        string title = "Título do Job",
        string description = "Descrição do job de exemplo.",
        State? state = null,
        City? city = null,
        Street? street = null,
        ZipCode? zipCode = null)
    {
        state ??= new State("SP");
        city ??= new City("São Paulo");
        street ??= new Street("Av. Exemplo");
        zipCode ??= new ZipCode("01001000");

        return Job.Create(contractorId, title, description, state, city, street, zipCode);
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

    [Fact]
    public void Create_SetsStatusToOpen()
    {
        var job = CreateJob();

        Assert.Equal(EJobStatus.Open, job.Status);
    }

    [Fact]
    public void Create_WithInvalidContractorId_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => CreateJob(contractorId: 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => CreateJob(contractorId: -1));
    }

    public static IEnumerable<object[]> NullOrWhitespaceTitles()
    {
        yield return new object[] { null! };
        yield return new object[] { "" };
        yield return new object[] { "   " };
    }

    [Theory]
    [MemberData(nameof(NullOrWhitespaceTitles))]
    public void Create_WithNullOrWhitespaceTitle_Throws(string? title)
    {
        Assert.Throws<ArgumentException>(() => CreateJob(title: title!));
    }

    [Fact]
    public void Create_WithTitleExceeding150Chars_Throws()
    {
        var title = new string('a', 151);
        Assert.Throws<ArgumentException>(() => CreateJob(title: title));
    }

    public static IEnumerable<object[]> NullOrWhitespaceDescriptions()
    {
        yield return new object[] { null! };
        yield return new object[] { "" };
        yield return new object[] { "   " };
    }

    [Theory]
    [MemberData(nameof(NullOrWhitespaceDescriptions))]
    public void Create_WithNullOrWhitespaceDescription_Throws(string? description)
    {
        Assert.Throws<ArgumentException>(() => CreateJob(description: description!));
    }

    [Fact]
    public void Create_WithDescriptionExceeding1000Chars_Throws()
    {
        var description = new string('a', 1001);
        Assert.Throws<ArgumentException>(() => CreateJob(description: description));
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
    public void Create_WithNullAddressArgument_Throws(string paramName)
    {
        var state = new State("SP");
        var city = new City("São Paulo");
        var street = new Street("Av. Exemplo");
        var zipCode = new ZipCode("01001000");

        Action act = paramName switch
        {
            "state" => () => Job.Create(1, "Título", "Descrição", null!, city, street, zipCode),
            "city" => () => Job.Create(1, "Título", "Descrição", state, null!, street, zipCode),
            "street" => () => Job.Create(1, "Título", "Descrição", state, city, null!, zipCode),
            "zipCode" => () => Job.Create(1, "Título", "Descrição", state, city, street, null!),
            _ => throw new InvalidOperationException("Invalid test case.")
        };

        var ex = Assert.Throws<ArgumentNullException>(act);
        Assert.Equal(paramName, ex.ParamName);
    }

    [Fact]
    public void UpdateTitle_WithNullOrWhitespace_Throws()
    {
        var job = CreateJob();
        Assert.Throws<ArgumentException>(() => job.UpdateTitle(null!));
        Assert.Throws<ArgumentException>(() => job.UpdateTitle(""));
        Assert.Throws<ArgumentException>(() => job.UpdateTitle("   "));
    }

    [Fact]
    public void UpdateTitle_WithTitleExceeding150Chars_Throws()
    {
        var job = CreateJob();
        Assert.Throws<ArgumentException>(() => job.UpdateTitle(new string('a', 151)));
    }

    [Fact]
    public void UpdateTitle_WhenDifferent_UpdatesTitleAndUpdatedAt()
    {
        var job = CreateJob();
        var oldUpdatedAt = job.UpdatedAt;

        job.UpdateTitle("Novo Título");

        Assert.Equal("Novo Título", job.Title);
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
    public void UpdateDescription_WithNullOrWhitespace_Throws()
    {
        var job = CreateJob();
        Assert.Throws<ArgumentException>(() => job.UpdateDescription(null!));
        Assert.Throws<ArgumentException>(() => job.UpdateDescription(""));
        Assert.Throws<ArgumentException>(() => job.UpdateDescription("   "));
    }

    [Fact]
    public void UpdateDescription_WithDescriptionExceeding1000Chars_Throws()
    {
        var job = CreateJob();
        Assert.Throws<ArgumentException>(() => job.UpdateDescription(new string('a', 1001)));
    }

    [Fact]
    public void UpdateDescription_WhenDifferent_UpdatesDescriptionAndUpdatedAt()
    {
        var job = CreateJob();
        var oldUpdatedAt = job.UpdatedAt;

        job.UpdateDescription("Nova descrição do job.");

        Assert.Equal("Nova descrição do job.", job.Description);
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
    public void UpdateAddress_WithNullState_Throws()
    {
        var job = CreateJob();
        Assert.Throws<ArgumentNullException>(() => job.UpdateAddress(null!, new City("SP"), new Street("Rua"), new ZipCode("01001000")));
    }

    [Fact]
    public void UpdateAddress_WithNullCity_Throws()
    {
        var job = CreateJob();
        Assert.Throws<ArgumentNullException>(() => job.UpdateAddress(new State("SP"), null!, new Street("Rua"), new ZipCode("01001000")));
    }

    [Fact]
    public void UpdateAddress_WithNullStreet_Throws()
    {
        var job = CreateJob();
        Assert.Throws<ArgumentNullException>(() => job.UpdateAddress(new State("SP"), new City("SP"), null!, new ZipCode("01001000")));
    }

    [Fact]
    public void UpdateAddress_WithNullZipCode_Throws()
    {
        var job = CreateJob();
        Assert.Throws<ArgumentNullException>(() => job.UpdateAddress(new State("SP"), new City("SP"), new Street("Rua"), null!));
    }

    [Fact]
    public void UpdateAddress_WhenDifferent_UpdatesAddressAndUpdatedAt()
    {
        var job = CreateJob();
        var oldUpdatedAt = job.UpdatedAt;

        var newState = new State("MG");
        var newCity = new City("Belo Horizonte");
        var newStreet = new Street("Rua Nova");
        var newZipCode = new ZipCode("30140071");

        job.UpdateAddress(newState, newCity, newStreet, newZipCode);

        Assert.Equal(newState, job.State);
        Assert.Equal(newCity, job.City);
        Assert.Equal(newStreet, job.Street);
        Assert.Equal(newZipCode, job.ZipCode);
        Assert.True(job.UpdatedAt > oldUpdatedAt);
    }

    [Fact]
    public void UpdateAddress_WhenSame_DoesNotUpdateUpdatedAt()
    {
        var job = CreateJob();
        var oldUpdatedAt = job.UpdatedAt;

        job.UpdateAddress(job.State, job.City, job.Street, job.ZipCode);

        Assert.Equal(oldUpdatedAt, job.UpdatedAt);
    }

    public static IEnumerable<object[]> ValidJobStatusTransitions()
    {
        yield return new object[] { EJobStatus.Open, EJobStatus.InProgress };
        yield return new object[] { EJobStatus.Open, EJobStatus.Closed };
        yield return new object[] { EJobStatus.InProgress, EJobStatus.Closed };
    }

    [Theory]
    [MemberData(nameof(ValidJobStatusTransitions))]
    public void UpdateStatus_WithValidTransition_UpdatesStatusAndUpdatedAt(EJobStatus from, EJobStatus to)
    {
        var job = CreateJob();
        if (from == EJobStatus.InProgress)
            job.UpdateStatus(EJobStatus.InProgress);

        var oldUpdatedAt = job.UpdatedAt;
        job.UpdateStatus(to);

        Assert.Equal(to, job.Status);
        Assert.True(job.UpdatedAt > oldUpdatedAt);
    }

    public static IEnumerable<object[]> InvalidJobStatusTransitions()
    {
        yield return new object[] { EJobStatus.Closed, EJobStatus.Open };
        yield return new object[] { EJobStatus.Closed, EJobStatus.InProgress };
        yield return new object[] { EJobStatus.InProgress, EJobStatus.Open };
    }

    [Theory]
    [MemberData(nameof(InvalidJobStatusTransitions))]
    public void UpdateStatus_WithInvalidTransition_Throws(EJobStatus from, EJobStatus to)
    {
        var job = CreateJob();
        if (from != EJobStatus.Open)
            job.UpdateStatus(from == EJobStatus.InProgress ? EJobStatus.InProgress : EJobStatus.Closed);

        Assert.Throws<InvalidOperationException>(() => job.UpdateStatus(to));
    }

    [Fact]
    public void UpdateStatus_WhenSame_DoesNotUpdateUpdatedAt()
    {
        var job = CreateJob();
        var oldUpdatedAt = job.UpdatedAt;

        job.UpdateStatus(job.Status);

        Assert.Equal(oldUpdatedAt, job.UpdatedAt);
    }
}