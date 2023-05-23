namespace CB.Domain.Entities;

public class User
{
    public int Id { get; }
    public string Email { get; }
    public string Username { get; }
    public string Firstname { get; }
    public string Lastname { get; }
    public string PasswordHash { get; }
    public string RefreshToken { get; }
    public string? AvatarURL { get; }

    public User(
        int id,
        string email,
        string username,
        string firstname,
        string lastname,
        string passwordHash,
        string refreshToken,
        string? avatarUrl
    )
    {
        Id = id;
        Email = email;
        Username = username;
        Firstname = firstname;
        Lastname = lastname;
        PasswordHash = passwordHash;
        RefreshToken = refreshToken;
        AvatarURL = avatarUrl;
    }
}