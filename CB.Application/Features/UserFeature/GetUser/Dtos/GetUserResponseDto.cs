namespace CB.Application.Features.UserFeature.GetUser.Dtos;

public record GetUserResponseDto
(
    string Email,
    string Username,
    string Firstname,
    string Lastname,
    string RefreshToken,
    string? AvatarURL
);