using CB.Application.Features.AuthFeature;
using CB.Application.Features.AuthFeature.Auth.Dtos;
using CB.Application.Features.AuthFeature.Refresh.Dtos;
using CB.Application.Features.AuthFeature.Signin.Dtos;
using CB.Application.Features.UserFeature.CreateUser.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CB.WebAPI.Controllers;

[ApiController]
[Route("auth")]
public class AuthController
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("signin")]
    public async Task<AuthResponseDto> Signin([FromBody] SigninDto signinDto)
    {
        return await _authService.Signin(signinDto);
    }

    [HttpPost("signup")]
    public async Task<AuthResponseDto> Signup([FromBody] CreateUserDto createUserDto)
    {
        return await _authService.Signup(createUserDto);
    }

    [HttpPost("refresh")]
    public async Task<RefreshTokensResponseDto> Refresh([FromBody] RefreshTokensDto refreshTokensDto)
    {
        return await _authService.Refresh(refreshTokensDto);
    }
}