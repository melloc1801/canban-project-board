using CB.Application.Features.UserFeature.CreateUser.Dtos;
using MediatR;

namespace CB.Application.Features.AuthFeature.Signup;

public class SignupCommandHandler: IRequestHandler<SignupCommand, CreateUserResponseDto>
{
    private readonly IAuthService _authService;

    public SignupCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public Task<CreateUserResponseDto> Handle(SignupCommand request, CancellationToken cancellationToken)
    {
        var dto = new CreateUserDto(
            request.Email, 
            request.Username, 
            request.Firstname, 
            request.Lastname, 
            request.Password, 
            request.AvatarURL
        );
        return _authService.Signup(dto);
    }
}