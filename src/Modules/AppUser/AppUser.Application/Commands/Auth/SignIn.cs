using AppUser.Shared.DTOs;
using MediatR;

namespace AppUser.Application.Commands.Auth
{
    public sealed record SignIn : IRequest<UserDto>
    {
    }
}
