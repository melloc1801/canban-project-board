using CB.Application.Features.AuthFeature.Auth.Dtos;
using CB.Application.Features.AuthFeature.Refresh.Dtos;
using CB.Application.Features.AuthFeature.Signin.Dtos;
using CB.Application.Features.UserFeature.CreateUser.Dtos;

namespace CB.Application.Features.AuthFeature;

public interface IAuthService
{
    Task<AuthResponseDto> Signin(SigninDto signinDto);
    Task<AuthResponseDto> Signup(CreateUserDto createUserDto);
    Task<RefreshTokensResponseDto> Refresh(RefreshTokensDto refreshTokensDto);
}