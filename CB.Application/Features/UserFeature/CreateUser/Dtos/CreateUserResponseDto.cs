namespace CB.Application.Features.UserFeature.CreateUser.Dtos;

public record CreateUserResponseDto
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