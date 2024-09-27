using AppUser.Shared.DTOs;
using MediatR;
using System;

namespace AppUser.Application.Commands.Auth
{
    public sealed record ChangeUsername(Guid UserId, string  Username) : IRequest<UserIdentityDto>;
}
