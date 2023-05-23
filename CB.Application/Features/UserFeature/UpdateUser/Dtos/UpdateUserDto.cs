namespace CB.Application.Features.UserFeature.UpdateUser.Dtos;

public record UpdateUserDto(
    string Email,
    string Username,
    string Firstname,
    string Lastname,
    string PasswordHash,
    string RefreshToken,
    string? AvatarURL
);