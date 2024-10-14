using Identity.Shared.DTOs;
using MediatR;

namespace Identity.Application.Commands.Auth
{
    public sealed record SignIn(string Email, string Password) : IRequest<UserIdentityDto>;
}
