using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Tests.Domain.ValueObjects
{
  public class NameTests
  {
    [Fact]
    public void CreateValidName_ShouldSucceed()
    {
      var firstName = "José";
      var lastName = "Leonardo";

      var name = new Name(firstName, lastName);

      Assert.Equal("José", name.FirstName);
      Assert.Equal("Leonardo", name.LastName);
      Assert.Equal("José Leonardo", name.FullName);
    }

    [Theory]
    [InlineData("", "Leonardo", "First name cannot be empty.")]
    [InlineData("Jo", "Leonardo", "First name is too short.")]
    [InlineData("José1", "Leonardo", "First name cannot contain numbers.")]
    [InlineData("José@", "Leonardo", "First name contains invalid characters.")]
    [InlineData("José", "", "Last name cannot be empty.")]
    [InlineData("José", "Le", "Last name is too short.")]
    [InlineData("José", "Leonardo2", "Last name cannot contain numbers.")]
    [InlineData("José", "Leonardo#", "Last name contains invalid characters.")]
    public void CreateInvalidName_ShouldThrowArgumentException(string firstName, string lastName, string expectedMessage)
    {
      var exception = Assert.Throws<ArgumentException>(() => new Name(firstName, lastName));
      Assert.Equal(expectedMessage, exception.Message);
    }

    [Fact]
    public void CreateName_TooLongFullName_ShouldThrowArgumentException()
    {
      var firstName = new string('A', 75);
      var lastName = new string('B', 76);

      var exception = Assert.Throws<ArgumentException>(() => new Name(firstName, lastName));
      Assert.Equal("Name is too long.", exception.Message);
    }

    [Fact]
    public void NameEquality_SameValues_ShouldBeEqual()
    {
      var name1 = new Name("Ana", "Silva");
      var name2 = new Name("Ana", "Silva");

      Assert.Equal(name1, name2);
    }

    [Fact]
    public void NameEquality_DifferentValues_ShouldNotBeEqual()
    {
      var name1 = new Name("Ana", "Silva");
      var name2 = new Name("Ana", "Sousa");

      Assert.NotEqual(name1, name2);
    }

    [Fact]
    public void NameToString_ShouldReturnFullName()
    {
      var name = new Name("Carlos", "Pereira");
      Assert.Equal("Carlos Pereira", name.ToString());
    }
  }
}