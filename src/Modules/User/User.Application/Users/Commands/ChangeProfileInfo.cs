using MediatR;

namespace User.Application.Users.Commands
{
    public sealed record ChangeProfileInfo() : IRequest
    {
    }
}
