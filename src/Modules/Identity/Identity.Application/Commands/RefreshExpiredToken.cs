using MediatR;

namespace Identity.Application.Commands.Auth
{
    public sealed record RefreshExpiredToken(string AccessToken, string RefreshToken) : IRequest<string>;
}
