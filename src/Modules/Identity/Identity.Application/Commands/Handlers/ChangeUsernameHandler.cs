using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Messaging;
using Identity.Shared.IntegrationEvents;
using Identity.Application.Services;
using Identity.Shared.DTOs;
using WorldDomination.Shared.Exceptions.CustomExceptions;
using WorldDomination.Shared.Services;

namespace Identity.Application.Commands.Users.Handlers
{
    internal class ChangeUsernameHandler : IRequestHandler<ChangeUsername, string>
    {
        private readonly ILogger<ChangeUsernameHandler> _logger;
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        private readonly IMessageBroker _messageBroker;
        private readonly IHttpContextService _httpContextService;

        public ChangeUsernameHandler(ILogger<ChangeUsernameHandler> logger, IAuthService authService,
            IMessageBroker messageBroker, ITokenService tokenService, IHttpContextService httpContextService)
        {
            _logger = logger;
            _authService = authService;
            _tokenService = tokenService;
            _messageBroker = messageBroker;
            _httpContextService = httpContextService;
        }

        public async Task<string> Handle(ChangeUsername command, CancellationToken cancellationToken)
        {
            // Identity user
            var username = command.Username;

            var userId = _httpContextService.GetCurrentUserId().ToString();

            if (!await _authService.UpdateUserName(userId, username))
            {
                throw new BadRequestException("Cannot change username");
            }

            _logger.LogInformation($"AuthUser {userId} changed username to {username}");

            await _messageBroker.PublishAsync(new UsernameChanged(userId, username), cancellationToken);

            return username;
        }
    }
}
