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
    [InlineData("", "Leonardo")]
    [InlineData("Jo", "Leonardo")]
    [InlineData("José1", "Leonardo")]
    [InlineData("José@", "Leonardo")]
    [InlineData("José", "")]
    [InlineData("José", "Le")]
    [InlineData("José", "Leonardo2")]
    [InlineData("José", "Leonardo#")]
    public void CreateInvalidName_ShouldThrowArgumentException(string firstName, string lastName)
    {
      var exception = Assert.Throws<ArgumentException>(() => new Name(firstName, lastName));
      Assert.Contains(exception.ParamName, new [] {"First name", "Last name"});
    }

    [Fact]
    public void CreateName_TooLongFullName_ShouldThrowArgumentException()
    {
      var firstName = new string('A', 75);
      var lastName = new string('B', 76);

      var exception = Assert.Throws<ArgumentException>(() => new Name(firstName, lastName));
      Assert.Equal("fullName", exception.ParamName);
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