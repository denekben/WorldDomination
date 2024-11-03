using Identity.Shared.IntegrationEvents;
using Microsoft.Extensions.Logging;
using Shared.Events;
using User.Domain.Entities;
using User.Domain.Repositories;
using WorldDomination.Shared.Domain;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace User.Application.Users.IntegrationEvents.Handlers
{
    internal class UsernameChangedHandler : IEventHandler<UsernameChanged>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UsernameChangedHandler> _logger;

        public UsernameChangedHandler(IUserRepository userRepository, ILogger<UsernameChangedHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task HandleAsync(UsernameChanged @event, CancellationToken cancellationToken = default)
        {
            var (userId, username) = @event;

            // Domain user
            var user = await _userRepository.GetAsync(new Guid(userId))
                ?? throw new BadRequestException($"Cannot find user {userId}");

            user.ChangeUsername(username);

            await _userRepository.UpdateAsync(user);

            _logger.LogInformation($"User {userId} changed username to {username}");
        }
    }
}