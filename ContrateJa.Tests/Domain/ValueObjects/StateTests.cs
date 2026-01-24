using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Tests.Domain.ValueObjects
{
  public class StateTests
  {
    [Fact]
    public void CreateState_WithValidCode_ShouldCreateState()
    {
      var state = new State("sp");

      Assert.Equal("SP", state.Code);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CreateState_NullOrEmpty_ShouldThrowArgumentException(string? code)
    {
      var ex = Assert.Throws<ArgumentException>(() => new State(code!));
      Assert.Equal("State is required.", ex.Message);
    }

    [Theory]
    [InlineData("XX")]
    [InlineData("ABC")]
    [InlineData("1A")]
    [InlineData("S")]
    public void CreateState_InvalidCode_ShouldThrowArgumentException(string code)
    {
      var ex = Assert.Throws<ArgumentException>(() => new State(code));
      Assert.Equal("Invalid state.", ex.Message);
    }

    [Fact]
    public void State_ShouldTrimAndUppercaseCode()
    {
      var state = new State("  rj  ");

      Assert.Equal("RJ", state.Code);
    }

    [Fact]
    public void States_WithSameCode_ShouldBeEqual()
    {
      var state1 = new State("MG");
      var state2 = new State("mg");

      Assert.Equal(state1, state2);
      Assert.Equal(state1.GetHashCode(), state2.GetHashCode());
    }

    [Fact]
    public void States_WithDifferentCodes_ShouldNotBeEqual()
    {
      var state1 = new State("SP");
      var state2 = new State("RJ");

      Assert.NotEqual(state1, state2);
    }

    [Fact]
    public void State_ToString_ShouldReturnCode()
    {
      var state = new State("BA");

      Assert.Equal("BA", state.ToString());
    }
  }
}