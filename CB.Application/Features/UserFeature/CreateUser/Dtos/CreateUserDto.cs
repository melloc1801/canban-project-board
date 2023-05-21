using System.ComponentModel.DataAnnotations;
using CB.Application.Validations.Attributes;

namespace CB.Application.Features.UserFeature.CreateUser.Dtos;

public record CreateUserDto(
    [Required(ErrorMessage = "Required")]
    [IsEmailAddress(ErrorMessage = "The email is invalid")]
    [MaxLength(320, ErrorMessage = "Max required email length is 320")]
    string Email,

    [Required]
    [MinLength(2, ErrorMessage = "Min required username length is 2")]
    [MaxLength(32, ErrorMessage = "Max required username length is 32")]
    string Username,

    [Required]
    [MinLength(2), MaxLength(50)]
    string Firstname,

    [Required]
    [MinLength(2), MaxLength(50)]
    string Lastname,

    [Required]
    [MinLength(10), MaxLength(64)]
    string Password,

    [Url] 
    string? AvatarURL
);
