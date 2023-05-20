namespace CB.Application.Features.JWTFeature;

public class JwtOptions
{
    public string AccessSecretKey { get; }
    public string RefreshSecretKey { get; }
    public int AccessTokenExpiresInMinutes { get; }
    public int RefreshTokenExpiresInDays { get; }


    public JwtOptions(
        string accessSecretKey, 
        string refreshSecretKey,
        int accessTokenExpiresInMinutes,
        int refreshTokenExpiresInDays
        )
    {
        if (accessSecretKey.Length < 32)
            throw new ArgumentException("Access token secret key length must be more then 32 digits");
        if (refreshSecretKey.Length < 32)
            throw new ArgumentException("Refresh token secret key length must be more then 32 digits");

        AccessSecretKey = accessSecretKey;
        RefreshSecretKey = refreshSecretKey;
        AccessTokenExpiresInMinutes = accessTokenExpiresInMinutes;
        RefreshTokenExpiresInDays = refreshTokenExpiresInDays;
    }
}
