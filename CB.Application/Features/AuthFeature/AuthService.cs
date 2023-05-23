using CB.Application.Exceptions;
using CB.Application.Exceptions.Http;
using CB.Application.Features.AuthFeature.Auth.Dtos;
using CB.Application.Features.AuthFeature.Signin.Dtos;
using CB.Application.Features.CryptoFeature;
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
    private readonly ICryptoService _cryptoService;

    public AuthService(IUserService userService, IJwtService jwtService, ICryptoService cryptoService)
    {
        _userService = userService;
        _jwtService = jwtService;
        _cryptoService = cryptoService;
    }

    public async Task<AuthResponseDto> Signin(SigninDto signinDto)
    {
        var user = await _userService.GetOneByEmail(signinDto.Email);
        if (user == null)
        {
            throw new BadRequestException(
                "User not found", 
                "User with email doesn't exists", 
                new FieldException[]
                {
                    new ("email", "Email doesn't exists")
                }
            );
        }

        var passwordHash = _cryptoService.ComputeSHA256Hash(signinDto.Password);
        if (passwordHash != user.PasswordHash)
        {
            throw new BadRequestException("Invalid password", "Invalid password", new FieldException[]
            {
                new ("Password", "Invalid password")
            });
        }

        var tokenPayload = new CreateTokenDto(user.Id, user.Email, user.Username);
        var newAccessToken = _jwtService.CreateAccessToken(tokenPayload);
        var newRefreshToken = _jwtService.CreateRefreshToken(tokenPayload);

        var updateUserDto = new UpdateUserDto(
            user.Email,
            user.Username,
            user.Firstname,
            user.Lastname,
            user.PasswordHash,
            newRefreshToken,
            user.AvatarURL
        );
        var updatedUser = await _userService.UpdateUser(user.Id, updateUserDto);

        return new AuthResponseDto(
            updatedUser.Id, 
            updatedUser.Email, 
            updatedUser.Username, 
            updatedUser.Firstname, 
            updatedUser.Lastname,
            newAccessToken,
            updateUserDto.RefreshToken,
            updatedUser.AvatarURL
        );
    }

    public async Task<AuthResponseDto> Signup(CreateUserDto createUserDto)
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

        return new AuthResponseDto(
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