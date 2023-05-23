using CB.Application.Features.AuthFeature.Signup.Dtos;
using CB.Application.Features.JWTFeature;
using CB.Application.Features.JWTFeature.Dtos;
using CB.Application.Features.UserFeature;
using CB.Application.Features.UserFeature.CreateUser.Dtos;
using CB.Application.Features.UserFeature.UpdateUser.Dtos;

namespace CB.Application.Features.AuthFeature;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;

    public AuthService(IUserService userService, IJwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }

    public async Task<SignupResponseDto> Signup(CreateUserDto createUserDto)
    {
        var user = await _userService.Create(createUserDto);

        var tokenPayload = new CreateTokenDto(user.Id, user.Email, user.Username);
        var accessToken = _jwtService.CreateAccessToken(tokenPayload);
        var refreshToken = _jwtService.CreateRefreshToken(tokenPayload);
        var updateUserDto = new UpdateUserDto(
            user.Email, 
            user.Username, 
            user.Firstname,
            user.Lastname,
            user.PasswordHash,
            refreshToken,
            createUserDto.AvatarURL
        );
        var updatedUser = await _userService.UpdateUser(user.Id, updateUserDto);

        return new SignupResponseDto(
            updatedUser.Id,
            updatedUser.Email,
            updatedUser.Username,
            updatedUser.Firstname,
            updatedUser.Lastname,
            accessToken,
            updatedUser.RefreshToken,
            updatedUser.AvatarURL
        );
    }
}