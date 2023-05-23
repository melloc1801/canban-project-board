using CB.Application.Features.UserFeature.CreateUser.Dtos;
using CB.Application.Features.UserFeature.UpdateUser.Dtos;
using CB.Domain.Entities;

namespace CB.Application.Repositories;

public interface IUserRepository
{
    public Task<User> Create(CreateUserEntityDto createUserDto);
    public Task<User?> GetOneByEmail(string email);
    public Task<User> Update(int id, UpdateUserDto updateUserDto);
}