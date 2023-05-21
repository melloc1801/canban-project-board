using CB.Domain.Entities;
using MediatR;

namespace CB.Application.Features.UserFeature.GetUser;

internal sealed class GetUserQueryHandler: IRequestHandler<GetUserQuery, User?>
{
    private readonly IUserService _userService;

    public GetUserQueryHandler(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task<User?> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        return await _userService.GetOneByEmail(request.Email);
    }
}