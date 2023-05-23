using System.ComponentModel.DataAnnotations;

namespace CB.Application.Features.AuthFeature.Refresh.Dtos;

public record RefreshTokensDto(
    [RegularExpression(@"[a-zA-z0-9]+\.[a-zA-z0-9]+\.[a-zA-z0-9]+", ErrorMessage = "Invalid token format")]
    string RefreshToken
);