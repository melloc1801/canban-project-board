using System.Reflection;
using CB.Application.Features.AuthFeature;
using CB.Application.Features.CryptoFeature;
using CB.Application.Features.JWTFeature;
using CB.Application.Features.UserFeature;

namespace CB.Application;

public static class ServiceExtensions
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<ICryptoService, CryptoService>();
        services.AddSingleton(sp =>
        {
            var jwtSection = sp.GetService<IConfiguration>()?.GetSection("JWT");
            var accessTokenSection = jwtSection?.GetSection("accessToken");
            var refreshTokenSection = jwtSection?.GetSection("refreshToken");
            var accessTokenExpiresInMinutes = accessTokenSection.GetValue<int>("expiresMinutes");
            var accessTokenSecretKey = accessTokenSection.GetValue<string>("secret");
            var refreshTokenExpiresInDays = refreshTokenSection.GetValue<int>("expiresDays");
            var refreshTokenSecretKey = refreshTokenSection.GetValue<string>("secret");
                
            
            return new JwtOptions(
                accessTokenSecretKey,
                refreshTokenSecretKey,
                accessTokenExpiresInMinutes,
                refreshTokenExpiresInDays
            );
        });
    }
}