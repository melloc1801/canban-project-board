using CB.Application.Exceptions;
using CB.Application.Exceptions.Http;
using CB.Application.Features.JWTFeature;
using CB.Application.Features.JWTFeature.Dtos;
using CB.Application.Features.UserFeature.CreateUser.Dtos;
using CB.Application.Features.UserFeature.UpdateUser.Dtos;
using CB.Application.Repositories;
using CB.Domain.Entities;

namespace CB.Application.Features.UserFeature;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public UserService(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }
    
    public async Task<CreateUserResponseDto> Create(CreateUserDto createUserDto)
    {
        var candidate = await GetOneByEmail(createUserDto.Email);
        if (candidate != null)
        {
            throw new BadRequestException(
                "User already exists", 
                "User with email already exists", 
                new []
                {
                    new FieldException(nameof(createUserDto.Email), "Email already exists")
                }
            );
        }
        var passwordHash = createUserDto.Password.GetHashCode().ToString();
        
        var createUserEntityDto = new CreateUserEntityDto(
            createUserDto.Email,
            createUserDto.Username,
            createUserDto.Firstname,
            createUserDto.Lastname,
            passwordHash,
            createUserDto.AvatarURL
        );
        await _userRepository.Create(createUserEntityDto);

        var user = await _userRepository.GetOneByEmail(createUserDto.Email);
        if (user == null)
        {
            throw new InternalServerException();
        }

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
        await _userRepository.Update(user.Id, updateUserDto);

        return new CreateUserResponseDto(
            user.Id,
            user.Email,
            user.Username,
            user.Firstname,
            user.Lastname,
            accessToken,
            refreshToken,
            user.AvatarURL
        );
    }

    public async Task<User?> GetOneByEmail(string email)
    {
        return await _userRepository.GetOneByEmail(email);
    }

    public Task UpdateUser(int id, UpdateUserDto updateUserDto)
    {
        throw new NotImplementedException();
    }
}