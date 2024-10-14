using Identity.Shared.IntegrationEvents;
using Microsoft.Extensions.Logging;
using Shared.Events;
using System;
using System.Threading;
using System.Threading.Tasks;
using User.Domain.Repositories;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace User.Application.Users.ExternalEvents.Handlers
{
    internal class UserDeletedHandler : IEventHandler<UserDeleted>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserDeletedHandler> _logger;

        public UserDeletedHandler(IUserRepository userRepository, ILogger<UserDeletedHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task HandleAsync(UserDeleted @event, CancellationToken cancellationToken = default)
        {
            var userId = @event.UserId;

            // Domain user
            var user = await _userRepository.GetAsync(new Guid(userId)) 
                ?? throw new BadRequestException("Cannot delete user");

            await _userRepository.DeleteAsync(user);
            _logger.LogInformation($"User {userId} deleted");
        }
    }
}
