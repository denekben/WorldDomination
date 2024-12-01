using Game.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using Shared.Events;
using User.Shared.IntegrationEvents;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Application.UseCases.Users.IntegrationEvents.Handlers
{
    internal sealed class NameChangedHandler : IEventHandler<NameChanged>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<NameChangedHandler> _logger;

        public NameChangedHandler(IUserRepository userRepository, ILogger<NameChangedHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task HandleAsync(NameChanged @event, CancellationToken cancellationToken = default)
        {
            var (userId, name) = @event;

            var user = await _userRepository.GetAsync(userId)
                ?? throw new BadRequestException($"Cannot find GameUser {@event.UserId}");

            user.ChangeName(name);
            await _userRepository.UpdateAsync(user);
            _logger.LogInformation($"GameUser {@event.Name} changed name");
        }
    }
}
