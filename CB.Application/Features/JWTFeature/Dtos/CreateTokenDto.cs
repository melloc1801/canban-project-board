namespace CB.Application.Features.JWTFeature.Dtos;

public record CreateTokenDto
(
    int Id,
    string Email,
    string Username
);