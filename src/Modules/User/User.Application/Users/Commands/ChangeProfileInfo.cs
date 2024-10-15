using MediatR;

namespace User.Application.Users.Commands
{
    public sealed record ChangeProfileInfo(string? Name, string? Bio) : IRequest;
}
