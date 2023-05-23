using CB.Application.Exceptions;
using CB.Application.Exceptions.Http;
using CB.Application.Features.CryptoFeature;
using CB.Application.Features.UserFeature.CreateUser.Dtos;
using CB.Application.Features.UserFeature.UpdateUser.Dtos;
using CB.Application.Repositories;
using CB.Domain.Entities;

namespace CB.Application.Features.UserFeature;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ICryptoService _cryptoService;

    public UserService(IUserRepository userRepository, ICryptoService cryptoService)
    {
        _userRepository = userRepository;
        _cryptoService = cryptoService;
    }
    
    public async Task<User> Create(CreateUserDto createUserDto)
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
        var passwordHash = _cryptoService.ComputeSHA256Hash(createUserDto.Password);
        
        var createUserEntityDto = new CreateUserEntityDto(
            createUserDto.Email,
            createUserDto.Username,
            createUserDto.Firstname,
            createUserDto.Lastname,
            passwordHash,
            createUserDto.AvatarURL
        );
        return await _userRepository.Create(createUserEntityDto);
    }

    public async Task<User?> GetOneByEmail(string email)
    {
        return await _userRepository.GetOneByEmail(email);
    }

    public async Task<User> UpdateUser(int id, UpdateUserDto updateUserDto)
    {
        return await _userRepository.Update(id, updateUserDto);
    }
}