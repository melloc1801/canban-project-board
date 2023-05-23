using CB.Application.Features.UserFeature.CreateUser.Dtos;
using CB.Application.Features.UserFeature.UpdateUser.Dtos;
using CB.Application.Repositories;
using CB.Domain.Entities;
using CB.Infrastructure.Persistence.common;
using Npgsql;

namespace CB.Infrastructure.Persistence.Repositories;

public class UserRepository : BaseRepository, IUserRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public UserRepository(string connectionString) : base(connectionString)
    {
        _dataSource = NpgsqlDataSource.Create(_connectionString);
    }

    public async Task<User> Create(CreateUserEntityDto createUserEntityDto)
    {
        var connection = await _dataSource.OpenConnectionAsync();

        await using var cmd = new NpgsqlCommand(
            "INSERT INTO \"User\"(email, avatarURL, username, firstname, lastname, passwordHash, refreshToken) VALUES (@email, @avatarURL, @username, @firstname, @lastname, @passwordHash, @refreshToken) RETURNING *",
            connection
        )
        {
            Parameters =
            {
                new("email", createUserEntityDto.Email),
                new("avatarURL", createUserEntityDto.AvatarURL == null ? DBNull.Value : createUserEntityDto.AvatarURL),
                new("username", createUserEntityDto.Username),
                new("passwordHash", createUserEntityDto.PasswordHash),
                new("firstname", createUserEntityDto.Firstname),
                new("lastname", createUserEntityDto.Lastname),
                new("refreshToken", DBNull.Value)
            }
        };
        try
        {
            var reader = await cmd.ExecuteReaderAsync();
            return await ReadUser(reader);
        }
        catch (PostgresException e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<User?> GetOneByEmail(string email)
    {
        var connection = await _dataSource.OpenConnectionAsync();

        await using var command = new NpgsqlCommand(
            "SELECT * FROM \"User\" WHERE email = @email LIMIT 1",
            connection
        )
        {
            Parameters =
            {
                new("email", email)
            }
        };
        var reader = await command.ExecuteReaderAsync();
        return await ReadUser(reader);
    }

    public async Task<User> Update(int id, UpdateUserDto updateUserDto)
    {
        var connection = await _dataSource.OpenConnectionAsync();

        await using var cmd = new NpgsqlCommand(
            "UPDATE \"User\" SET email = @email, username = @username, firstname = @firstname, lastname = @lastname, passwordHash = @passwordHash, refreshToken = @refreshToken WHERE id = @id RETURNING *",
            connection
        )
        {
            Parameters =
            {
                new("id", id),
                new("email", updateUserDto.Email),
                new("username", updateUserDto.Username),
                new("firstname", updateUserDto.Firstname),
                new("lastname", updateUserDto.Lastname),
                new("passwordHash", updateUserDto.PasswordHash),
                new("refreshToken", updateUserDto.RefreshToken)
            }
        };

        var reader = await cmd.ExecuteReaderAsync();
        return await ReadUser(reader);
    }

    private async Task<User?> ReadUser(NpgsqlDataReader reader)
    {
        User result = null;
        while (await reader.ReadAsync())
        {
            result = new User(
                int.Parse(reader["id"].ToString()),
                reader["email"].ToString(),
                reader["username"].ToString(),
                reader["firstname"].ToString(),
                reader["lastname"].ToString(),
                reader["passwordHash"].ToString(),
                reader["refreshToken"].ToString(),
                reader["avatarURL"] == DBNull.Value ? null : reader["avatarURL"].ToString()
            );
        }

        return result;
    }
}