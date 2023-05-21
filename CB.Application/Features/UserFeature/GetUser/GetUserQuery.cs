using CB.Domain.Entities;
using MediatR;

namespace CB.Application.Features.UserFeature.GetUser;

public sealed record GetUserQuery(string Email): IRequest<User>;