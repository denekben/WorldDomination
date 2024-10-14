using Identity.Application.Services;
using Microsoft.Extensions.Logging;
using MediatR;
using Shared.Messaging;
using Identity.Shared.IntegrationEvents;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Identity.Application.Commands.Users.Handlers
{
    internal class DeleteUserHandler : IRequestHandler<DeleteUser>
    {
        private readonly ILogger<DeleteUserHandler> _logger;
        private readonly IAuthService _authService;
        private readonly IMessageBroker _messageBroker;
        private readonly IHttpContextService _contextService;

        public DeleteUserHandler(ILogger<DeleteUserHandler> logger, IAuthService authService,
        IMessageBroker messageBroker, IHttpContextService contextService)
        {
            _logger = logger;
            _authService = authService;
            _messageBroker = messageBroker;
            _contextService = contextService;
        }

        public async Task Handle(DeleteUser command, CancellationToken cancellationToken)
        {
            var userId = _contextService.GetCurrentUserId().ToString();

            // Identity user
            if (!await _authService.DeleteUserAsync(userId))
            {
                throw new BadRequestException("Cannot delete user");
            }

            await _messageBroker.PublishAsync(new UserDeleted(userId), cancellationToken);

            _logger.LogInformation($"AuthUser {userId} deleted");
        }
    }
}
