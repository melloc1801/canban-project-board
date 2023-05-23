namespace CB.Application.Features.AuthFeature.Auth.Dtos;

public record AuthResponseDto
(
    int Id,
    string Email,
    string Username,
    string Firstname,
    string Lastname,
    string AccessToken,
    string RefreshToken,
    string? AvatarURL
);