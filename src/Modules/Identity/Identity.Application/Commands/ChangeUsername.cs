using MediatR;

namespace Identity.Application.Commands.Users
{
    public sealed record ChangeUsername(string Username) : IRequest<string>;
}
