using CB.Application.Features.UserFeature.CreateUser.Dtos;

namespace CB.Application.Features.AuthFeature;

public interface IAuthService
{
    Task<CreateUserResponseDto> Signup(CreateUserDto createUserDto);
}