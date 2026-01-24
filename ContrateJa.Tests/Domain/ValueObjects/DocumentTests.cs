using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Tests.Domain.ValueObjects
{
  public class DocumentTests
  {
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Document_WithNullOrEmpty_ShouldThrow(string? value)
    {
      var ex = Assert.Throws<ArgumentException>(() => new Document(value!));
      Assert.Equal("Document is required.", ex.Message);
    }

    [Theory]
    [InlineData("123")]
    [InlineData("123456789")]
    [InlineData("1234567890123")]
    [InlineData("123456789012345")]
    public void Document_WithInvalidLength_ShouldThrow(string value)
    {
      var ex = Assert.Throws<ArgumentException>(() => new Document(value));
      Assert.Equal("Invalid document length.", ex.Message);
    }

    [Theory]
    [InlineData("111.111.111-11")]
    [InlineData("123.456.789-00")]
    public void Document_WithInvalidCpf_ShouldThrow(string cpf)
    {
      var ex = Assert.Throws<ArgumentException>(() => new Document(cpf));
      Assert.Equal("Invalid CPF.", ex.Message);
    }

    [Theory]
    [InlineData("11.111.111/1111-11")]
    [InlineData("12.345.678/0001-00")]
    public void Document_WithInvalidCnpj_ShouldThrow(string cnpj)
    {
      var ex = Assert.Throws<ArgumentException>(() => new Document(cnpj));
      Assert.Equal("Invalid CNPJ.", ex.Message);
    }

    [Theory]
    [InlineData("529.982.247-25", "52998224725")]
    [InlineData("39053344705", "39053344705")]
    public void Document_WithValidCpf_ShouldCreateDocument(string input, string expectedValue)
    {
      var document = new Document(input);

      Assert.Equal(expectedValue, document.Value);
      Assert.Equal(EDocumentType.CPF, document.Type);
    }

    [Theory]
    [InlineData("04.252.011/0001-10", "04252011000110")]
    [InlineData("40.688.134/0001-61", "40688134000161")]
    public void Document_WithValidCnpj_ShouldCreateDocument(string input, string expectedValue)
    {
      var document = new Document(input);

      Assert.Equal(expectedValue, document.Value);
      Assert.Equal(EDocumentType.CNPJ, document.Type);
    }

    [Fact]
    public void Document_WithSameValue_ShouldBeEqual()
    {
      var doc1 = new Document("529.982.247-25");
      var doc2 = new Document("52998224725");

      Assert.Equal(doc1, doc2);
    }

    [Fact]
    public void Document_WithDifferentValues_ShouldNotBeEqual()
    {
      var doc1 = new Document("529.982.247-25");
      var doc2 = new Document("390.533.447-05");

      Assert.NotEqual(doc1, doc2);
    }

    [Fact]
    public void Document_ToString_ShouldReturnValue()
    {
      var document = new Document("529.982.247-25");

      Assert.Equal("52998224725", document.ToString());
    }
  }
}