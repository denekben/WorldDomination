using MediatR;
using System;

namespace AppUser.Application.Commands.Auth
{
    public sealed record RefreshExpiredToken(Guid UserId) : IRequest<string>;
}
