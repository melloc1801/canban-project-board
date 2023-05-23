using CB.Application.Features.AuthFeature.Signup.Dtos;
using CB.Application.Features.UserFeature.CreateUser.Dtos;

namespace CB.Application.Features.AuthFeature;

public interface IAuthService
{
    Task<SignupResponseDto> Signup(CreateUserDto createUserDto);
}