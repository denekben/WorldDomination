using Identity.Application.DTOs;
using MediatR;

namespace Identity.Application.Commands
{
    public sealed record SignIn(string Email, string Password) : IRequest<UserIdentityDto>;
}
