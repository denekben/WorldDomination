using AppUser.Shared.DTOs;
using MediatR;

namespace AppUser.Application.Commands.Auth
{
    public sealed record RegisterNewUser(string Username, string Password, string Email) : IRequest<UserIdentityDto>;
}
