using AppUser.Application.Exceptions;
using AppUser.Application.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AppUser.Application.Commands.Auth.Handlers
{
    internal class RefreshExpiredTokenHandler : IRequestHandler<RefreshExpiredToken, string>
    {
        private readonly ILogger<RefreshExpiredTokenHandler> _logger;
        private readonly IAuthService _authService;

        public RefreshExpiredTokenHandler(ILogger<RefreshExpiredTokenHandler> logger,
            IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        public async Task<string> Handle(RefreshExpiredToken command, CancellationToken cancellationToken)
        {
            var token = await _authService.RefreshExpiredToken(command.UserId.ToString());
            if (token == null)
            {
                throw new BadRequestException("Cannot refresh token");
            }
            _logger.LogInformation($"AuthUser {command.UserId} refreshed expired token");

            return token;
        }
    }
}
