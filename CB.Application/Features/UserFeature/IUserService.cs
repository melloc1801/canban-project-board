using CB.Application.Features.UserFeature.CreateUser.Dtos;
using CB.Application.Features.UserFeature.UpdateUser.Dtos;
using CB.Domain.Entities;

namespace CB.Application.Features.UserFeature;

public interface IUserService
{
    Task<CreateUserResponseDto> Create(CreateUserDto createUserDto);
    Task<User?> GetOneByEmail(string email);
    Task UpdateUser(int id, UpdateUserDto updateUserDto);
}