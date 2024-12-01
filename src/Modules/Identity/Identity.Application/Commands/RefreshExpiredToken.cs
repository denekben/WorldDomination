using MediatR;

namespace Identity.Application.Commands
{
    public sealed record RefreshExpiredToken(string AccessToken, string RefreshToken) : IRequest<string>;
}
