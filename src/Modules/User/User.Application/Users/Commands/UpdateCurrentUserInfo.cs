using MediatR;

namespace Users.Application.Users.Commands
{
    public sealed record UpdateCurrentUserInfo(string? Name, string? Bio) : IRequest;
}