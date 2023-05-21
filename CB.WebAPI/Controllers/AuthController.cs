using CB.Application.Features.AuthFeature.Signup;
using CB.Application.Features.UserFeature.CreateUser.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CB.WebAPI.Controllers;

[ApiController]
[Route("auth")]
public class AuthController
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<CreateUserResponseDto> Signup([FromBody] CreateUserDto createUserDto)
    {
        return await _mediator.Send(new SignupCommand(
            createUserDto.Username,
            createUserDto.Email,
            createUserDto.Password,
            createUserDto.Firstname,
            createUserDto.Lastname,
            createUserDto.AvatarURL
        ));
    }
}