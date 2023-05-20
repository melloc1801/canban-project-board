namespace CB.Application.Features.JWTFeature.Dtos;

public record VerifyTokenResponseDto 
(
    int Id,
    string Email,
    string Username
);