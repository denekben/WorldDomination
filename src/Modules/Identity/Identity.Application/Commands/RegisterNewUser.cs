using Identity.Shared.DTOs;
using MediatR;

namespace Identity.Application.Commands.Auth
{
    public sealed record RegisterNewUser(string Username, string Password, string Email) : IRequest<UserIdentityDto>;
}
