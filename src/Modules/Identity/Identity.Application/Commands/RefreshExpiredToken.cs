using MediatR;

namespace Identity.Application.Commands.Auth
{
    public sealed record RefreshExpiredToken : IRequest<string>;
}
