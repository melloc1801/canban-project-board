using CB.Application.Features.JWTFeature.Dtos;

namespace CB.Application.Features.JWTFeature;

public interface IJwtService
{
    string CreateAccessToken(CreateTokenDto createTokenDto);
    string CreateRefreshToken(CreateTokenDto createTokenDto);
    CreateTokenDto? VerifyAccessToken(string token);
    CreateTokenDto VerifyRefreshToken(string token);
}