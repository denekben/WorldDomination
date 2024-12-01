using Identity.Application.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using WorldDomination.Shared.Exceptions.CustomExceptions;
using WorldDomination.Shared.Services;

namespace Identity.Application.Commands.Handlers
{
    internal class RefreshExpiredTokenHandler : IRequestHandler<RefreshExpiredToken, string>
    {
        private readonly ILogger<RefreshExpiredTokenHandler> _logger;
        private readonly IAuthService _authService;
        private readonly IHttpContextService _httpContextService;
        private readonly ITokenService _tokenService;

        public RefreshExpiredTokenHandler(ILogger<RefreshExpiredTokenHandler> logger,
            IAuthService authService,
            IHttpContextService httpContextService,
            ITokenService tokenService)
        {
            _logger = logger;
            _authService = authService;
            _httpContextService = httpContextService;
            _tokenService = tokenService;
        }

        public async Task<string> Handle(RefreshExpiredToken command, CancellationToken cancellationToken)
        {
            var username = _tokenService.GetPrincipalFromExpiredToken(command.AccessToken)
                .Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.GivenName))?.Value ??
                throw new BadRequestException("Cannot refresh token");

            if(!await _authService.IsRefreshTokenValid(username, command.RefreshToken))
            {
                throw new BadRequestException("Refresh token is invalid");
            }

            var (userId, _, email, roles) = await _authService.GetUserDetailsByUserNameAsync(username);
            string accessToken = _tokenService.GenerateAccessToken(userId, email, username, roles)
                ?? throw new BadRequestException("Cannot generate access token");

            _logger.LogInformation($"AuthUser {userId} refreshed expired token");

            return accessToken;
        }
    }
}
