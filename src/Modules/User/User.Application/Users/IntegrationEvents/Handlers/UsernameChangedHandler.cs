using Identity.Shared.IntegrationEvents;
using Microsoft.Extensions.Logging;
using Shared.Events;
using System;
using System.Threading;
using System.Threading.Tasks;
using User.Domain.Entities;
using WorldDomination.Shared.Domain;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace User.Application.Users.IntegrationEvents.Handlers
{
    internal class UsernameChangedHandler : IEventHandler<UsernameChanged>
    {
        private readonly IRepository<DomainUser> _userRepository;
        private readonly ILogger<UsernameChangedHandler> _logger;

        public UsernameChangedHandler(IRepository<DomainUser> userRepository, ILogger<UsernameChangedHandler> logger)
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
