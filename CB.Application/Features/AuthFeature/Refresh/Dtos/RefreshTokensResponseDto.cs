namespace CB.Application.Features.AuthFeature.Refresh.Dtos;

public record RefreshTokensResponseDto(
    string AccessToken,
    string RefreshToken
);