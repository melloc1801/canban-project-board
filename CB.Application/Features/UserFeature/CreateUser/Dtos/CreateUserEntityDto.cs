namespace CB.Application.Features.UserFeature.CreateUser.Dtos;

public record CreateUserEntityDto
(
    string Email,
    string Username,
    string Firstname,
    string Lastname,
    string PasswordHash,
    string? AvatarURL
);