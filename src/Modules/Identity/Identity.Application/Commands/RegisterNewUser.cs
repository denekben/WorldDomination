using Identity.Shared.DTOs;
using MediatR;

namespace Identity.Application.Commands.Auth
{
    public sealed record RegisterNewUser(string Username, string Email, string Password) : IRequest<UserIdentityDto>;
}
