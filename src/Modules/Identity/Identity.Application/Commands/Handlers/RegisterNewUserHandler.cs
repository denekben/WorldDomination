using Identity.Application.Services;
using Identity.Shared.DTOs;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Messaging;
using Identity.Shared.IntegrationEvents;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Identity.Application.Commands.Auth.Handlers
{
    internal class RegisterNewUserHandler : IRequestHandler<RegisterNewUser, UserIdentityDto>
    {
        private readonly ILogger<RegisterNewUserHandler> _logger;
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        private readonly IMessageBroker _messageBroker;

        public RegisterNewUserHandler(ILogger<RegisterNewUserHandler> logger, IAuthService authService,
            ITokenService tokenService, IMessageBroker messageBroker)
        {
            _tokenService = tokenService;
            _logger = logger;
            _authService = authService;
            _messageBroker = messageBroker;
        }

        public async Task<UserIdentityDto> Handle(RegisterNewUser command, CancellationToken cancellationToken)
        {
            // Identity User creating
            var (username, password, email) = command;

            var (result, userId) = await _authService.CreateUserAsync(username, password, email);
            if (!result)
            {
                throw new BadRequestException("Cannot create user");
            }
            _logger.LogInformation($"AuthUser {userId} registered");

            // Identity User roles
            await _authService.AssignUserToRole(email, "Member");
            if(!result)
            {
                throw new BadRequestException("Cannot assign user to role");
            }
            _logger.LogInformation($"AuthUser {userId} assigned to role Member");

            // Identity User tokens
            await _authService.UpdateRefreshToken(userId, _tokenService.GenerateRefreshToken());
            string token = _tokenService.GenerateAccessToken(userId, email, username, await _authService.GetUserRolesAsync(userId))
                ?? throw new BadRequestException("Cannot generate access token");

            await _messageBroker.PublishAsync(new NewUserRegistered(userId, username, email));

            return new UserIdentityDto(new Guid(userId), username, token);
        }
    }
}
