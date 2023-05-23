using CB.Application.Features.AuthFeature;
using CB.Application.Features.AuthFeature.Signup.Dtos;
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

    [HttpPost]
    public async Task<SignupResponseDto> Signup([FromBody] CreateUserDto createUserDto)
    {
        return await _authService.Signup(createUserDto);
    }
}