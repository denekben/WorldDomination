using Identity.Application.DTOs;
using MediatR;

namespace Identity.Application.Commands
{
    public sealed record RegisterNewUser(string Username, string Email, string Password) : IRequest<UserIdentityDto>;
}
