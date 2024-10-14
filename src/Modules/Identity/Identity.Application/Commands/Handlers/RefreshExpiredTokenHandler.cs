using Identity.Application.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Identity.Application.Commands.Auth.Handlers
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
            var userId = _httpContextService.GetCurrentUserId().ToString();
            if(await _authService.RefreshTokenExpired(userId))
            {
                throw new BadRequestException("Refresh token expired");
            }

            await _authService.UpdateRefreshToken(userId, _tokenService.GenerateRefreshToken());

            var (_, username, email, roles) = await _authService.GetUserDetailsAsync(userId);
            string token = _tokenService.GenerateAccessToken(userId, email, username, roles)
                ?? throw new BadRequestException("Cannot generate access token");

            _logger.LogInformation($"AuthUser {userId} refreshed expired token");

            return token;
        }
    }
}
