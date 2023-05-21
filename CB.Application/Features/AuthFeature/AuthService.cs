using CB.Application.Features.UserFeature;
using CB.Application.Features.UserFeature.CreateUser.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CB.Application.Features.AuthFeature;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;

    public AuthService(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<CreateUserResponseDto> Signup([FromBody] CreateUserDto createUserDto)
    {
        return await _userService.Create(createUserDto);
    }
}