using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CB.Application.Features.JWTFeature.Dtos;
using CB.Application.Features.JWTFeature.Enums;
using Microsoft.IdentityModel.Tokens;

namespace CB.Application.Features.JWTFeature;

public class JwtService: IJwtService
{
    private readonly JwtOptions _options;

    public JwtService(JwtOptions options)
    {
        _options = options;
    }
    
    public string CreateAccessToken(CreateTokenDto createTokenDto)
    {
        return CreateToken(createTokenDto, TokenType.Access);
    }

    public string CreateRefreshToken(CreateTokenDto createTokenDto)
    {
        return CreateToken(createTokenDto, TokenType.Refresh);
    }

    public CreateTokenDto? VerifyAccessToken(string token)
    {
        return ValidateToken(token, TokenType.Access);
    }

    public CreateTokenDto VerifyRefreshToken(string token)
    {
        return ValidateToken(token, TokenType.Refresh);
    }

    private string CreateToken(CreateTokenDto createTokenDto, TokenType tokenType)
    {
        var secretKey = "";
        var expires = DateTime.UtcNow;
        switch (tokenType)
        {
            case TokenType.Access:
                expires = DateTime.UtcNow.AddMinutes(_options.AccessTokenExpiresInMinutes);
                secretKey = _options.AccessSecretKey;
                break;
            case TokenType.Refresh:
                expires = DateTime.UtcNow.AddDays(_options.RefreshTokenExpiresInDays);
                secretKey = _options.RefreshSecretKey;
                break;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(
                new []
                {
                    new Claim("id", createTokenDto.Id.ToString()),
                    new Claim("email", createTokenDto.Email),
                    new Claim("username", createTokenDto.Username)
                }
            ),
            Expires = expires,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256
            )
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private CreateTokenDto ValidateToken(string token, TokenType tokenType)
    {
        if (token == null) return null;
        var secretKey = "";
        switch (tokenType)
        {
            case TokenType.Access:
                secretKey = _options.AccessSecretKey;
                break;
            case TokenType.Refresh:
                secretKey = _options.RefreshSecretKey;
                break;
        }
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secretKey);
        try
        {
            tokenHandler.ValidateToken(
                token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                },
                out SecurityToken validatedToken
            );

            var jwtToken = (JwtSecurityToken) validatedToken;
            var id = jwtToken.Claims.First(prop => prop.Type == "id").Value;
            var email = jwtToken.Claims.First(prop => prop.Type == "email").Value;
            var username = jwtToken.Claims.First(prop => prop.Type == "username").Value;

            return new CreateTokenDto(int.Parse(id), email, username);
        }
        catch
        {
            return null;
        }
    }
}