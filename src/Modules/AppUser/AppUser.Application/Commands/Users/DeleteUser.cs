using MediatR;

namespace AppUser.Application.Commands.Users
{
    public sealed record DeleteUser(string UserId) : IRequest;
}
