using CB.Application.Features.UserFeature.CreateUser.Dtos;
using MediatR;

namespace CB.Application.Features.AuthFeature.Signup;

public record SignupCommand(
    string Username,
    string Email,
    string Password,
    string Firstname,
    string Lastname,
    string? AvatarURL
): IRequest<CreateUserResponseDto>;