using CB.Application.Features.CryptoFeature;
using NUnit.Framework;

namespace CB.Tests;

[TestFixture]
public class CryptoServiceTests
{
    private ICryptoService _cryptoService;
    
    [SetUp]
    public void Init()
    {
        _cryptoService = new CryptoService();
    }

    [Test]
    public void ComputeSHA256Hash_Should_Throw_ArgumentNullException()
    {
        // Arrange
        string s = null;

        // Act
        Assert.Throws<ArgumentNullException>(() => _cryptoService.ComputeSHA256Hash(s));
    }

    [TestCase("")]
    [TestCase("s")]
    [TestCase("Hello, world!")]
    public void ComputeSHA256Hash_Should_ReturnStringWithTheSameLength(string str)
    {
        // Arrange
        var expectedHashLength = 64;

        // Act
        var hashLength = _cryptoService.ComputeSHA256Hash(str).Length;

        // Assert
        Assert.That(hashLength, Is.EqualTo(expectedHashLength));
    }

    [Test]
    public void ComputeSHA256Hash_Should_ReturnTheSameOutputWithTheSameInput()
    {
        // Arrange
        var str = "string_to_be_hashed";

        // Act
        var hashOne = _cryptoService.ComputeSHA256Hash(str);
        var hashTwo = _cryptoService.ComputeSHA256Hash(str);

        // Assert
        Assert.That(hashOne, Is.EqualTo(hashTwo));
    }
}