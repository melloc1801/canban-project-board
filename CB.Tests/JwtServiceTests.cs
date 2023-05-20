using CB.Application.Features.JWTFeature;
using CB.Application.Features.JWTFeature.Dtos;
using NUnit.Framework;

namespace CB.Tests;

[TestFixture]
public class JwtServiceTests
{
    private static CreateTokenDto _payload = new CreateTokenDto(1, "mail@mail.ru", "username");
    private IJwtService _jwtService;
    
    [SetUp]
    public void Setup()
    {
        _jwtService = new JwtService(
            new JwtOptions(
                "HERE_SHOULD_BE_ACCESS_TOKEN_SECRET_KEY",
                "HERE_SHOULD_BE_REFRESH_TOKEN_SECRET_KEY",
                15,
                30
            )
        );
    }
    
    [Test]
    public void CreateAccessToken_Should_CreateToken()
    {
        // Arrange
        var payload = new CreateTokenDto(1, "email@email.com", "username");
       
        // Act
        var _ = _jwtService.CreateAccessToken(payload);
    }
    
    [Test]
    public void RefreshAccessToken_Should_CreateToken()
    {
        var payload = new CreateTokenDto(1, "email@email.com", "username");
        var _ = _jwtService.CreateAccessToken(payload);
    }

    [TestCase(1, "email@email.com", "username")]
    [TestCase(123, "test@test.test", "_______")]
    public void AccessToken_Should_ConsistsOfThreeParts(int id, string email, string username)
    {
        // Arrange
        var token = _jwtService.CreateAccessToken(new CreateTokenDto(id, email, username));
        var expectedPartsAmount = 3;
        
        // Act
        var partsAmount = token.Split(".").Length;

        // Assert
        Assert.That(partsAmount, Is.EqualTo(expectedPartsAmount));
    }
    
    [TestCase(1, "email@email.com", "username")]
    [TestCase(123, "test@test.test", "_______")]
    public void RefreshToken_Should_ConsistsOfThreeParts(int id, string email, string username)
    {
        // Arrange
        var token = _jwtService.CreateRefreshToken(new CreateTokenDto(id, email, username));
        var expectedPartsAmount = 3;
        
        // Act
        var partsAmount = token.Split(".").Length;

        // Assert
        Assert.That(partsAmount, Is.EqualTo(expectedPartsAmount));
    }
    
    
    [TestCase(1, "email@email.com", "username")]
    [TestCase(123, "test@test.test", "_______")]
    public void AccessTokenFirstTwoParts_ShouldBe_TheSameWithTheSameInputValues(int id, string email, string username)
    {
        // Arrange
        var tokenOne = _jwtService.CreateAccessToken(new CreateTokenDto(id, email, username));
        var tokenTwo = _jwtService.CreateAccessToken(new CreateTokenDto(id, email, username));
        
        // Act
        var tokenOneParts = tokenOne.Split(".").Take(2).ToArray();
        var tokenTwoParts = tokenTwo.Split(".").Take(2).ToArray();
        
        // Assert
        CollectionAssert.AreEqual(tokenOneParts, tokenTwoParts);
    }
    
    [TestCase(1, "email@email.com", "username")]
    [TestCase(123, "test@test.test", "_______")]
    public void RefreshTokenFirstTwoParts_ShouldBe_TheSameWithTheSameInputValues(int id, string email, string username)
    {
        // Arrange
        var tokenOne = _jwtService.CreateRefreshToken(new CreateTokenDto(id, email, username));
        var tokenTwo = _jwtService.CreateRefreshToken(new CreateTokenDto(id, email, username));
        
        // Act
        var tokenOneParts = tokenOne.Split(".").Take(2).ToArray();
        var tokenTwoParts = tokenTwo.Split(".").Take(2).ToArray();

        // Assert
        CollectionAssert.AreEqual(tokenOneParts, tokenTwoParts);
    }

    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJlbWFpbCI6Im1haWxAbWFpbC5ydSIsInVzZXJuYW1lIjoidXNlcm5hbWUiLCJuYmYiOjE2ODQ1OTQ4MzQsImV4cCI6MTY4NDU5NDg5NCwiaWF0IjoxNjg0NTk0ODM0fQ.dQbn8DtdZxFJIcmgYAyizMomHfCkfIzTdw-tEOcIyeU")]
    [TestCase("jjJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.jjJpZCI6IjEiLCJlbWFpbCI6Im1haWxAbWFpbC5ydSIsInVzZXJuYW1lIjoidXNlcm5hbWUiLCJuYmYiOjE2ODQ1OTQ4MzQsImV4cCI6MTY4NDU5NDg5NCwiaWF0IjoxNjg0NTk0ODM0fQ.jjbn8DtdZxFJIcmgYAyizMomHfCkfIzTdw-tEOcIyeU")]
    public void VerifyAccessToken_Should_ReturnNull(string token)
    {
        // Act
        var actual = _jwtService.VerifyAccessToken(token);

        // Assert
        Assert.IsNull(actual);
    }
    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJlbWFpbCI6Im1haWxAbWFpbC5ydSIsInVzZXJuYW1lIjoidXNlcm5hbWUiLCJuYmYiOjE2ODQ1OTkyOTEsImV4cCI6MTY4NzE5MTI5MSwiaWF0IjoxNjg0NTk5MjkxfQ.apq2jkllio65RkkmUkJK1020jfnqmzqWmdskHJ9sjsi")]
    [TestCase("jjJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.jjJpZCI6IjEiLCJlbWFpbCI6Im1haWxAbWFpbC5ydSIsInVzZXJuYW1lIjoidXNlcm5hbWUiLCJuYmYiOjE2ODQ1OTkyOTEsImV4cCI6MTY4NzE5MTI5MSwiaWF0IjoxNjg0NTk5MjkxfQ.jjOSotY17g5abK4P0QnSRiKN53PbEhsNB8v8uGhSvBM")]
    public void VerifyRefreshToken_Should_ReturnNull(string token)
    {
        // Act
        var actual = _jwtService.VerifyRefreshToken(token);

        // Assert
        Assert.IsNull(actual);
    }

    [Test]
    public void VerifyAccessToken_Should_CheckWhetherTokenIsValid()
    {
        var token = _jwtService.CreateAccessToken(_payload);

        var actual = _jwtService.VerifyAccessToken(token);
        
        Assert.That(actual, Is.EqualTo(_payload));
    }
    
    [Test]
    public void VerifyRefreshToken_Should_CheckWhetherTokenIsValid()
    {
        var token = _jwtService.CreateRefreshToken(_payload);

        var actual = _jwtService.VerifyRefreshToken(token);
        
        Assert.That(actual, Is.EqualTo(_payload));
    }
}