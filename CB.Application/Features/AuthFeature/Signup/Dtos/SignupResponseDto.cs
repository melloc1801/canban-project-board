namespace CB.Application.Features.AuthFeature.Signup.Dtos;

public record SignupResponseDto
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