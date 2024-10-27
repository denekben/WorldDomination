using Identity.Shared.IntegrationEvents;
using Microsoft.Extensions.Logging;
using Shared.Events;
using Shared.Messaging;
using User.Domain.Entities;
using User.Shared.IntegrationEvents;
using WorldDomination.Shared.Domain;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace User.Application.Users.IntegrationEvents.Handlers
{
    internal class NewUserRegisteredHandler : IEventHandler<NewUserRegistered>
    {
        private readonly IRepository<DomainUser> _userRepository;
        private readonly ILogger<NewUserRegisteredHandler> _logger;
        private readonly IRepository<UserStatus> _userStatusRepository;
        private readonly IMessageBroker _messageBroker;

        public NewUserRegisteredHandler(IRepository<DomainUser> userRepository, ILogger<NewUserRegisteredHandler> logger,
            IRepository<UserStatus> userStatusRepository, IMessageBroker messageBroker)
        {
            _userRepository = userRepository;
            _logger = logger;
            _userStatusRepository = userStatusRepository;
            _messageBroker = messageBroker;
        }

        public async Task HandleAsync(NewUserRegistered @event, CancellationToken cancellationToken = default)
        {
            var (userId, username, email) = @event;

            // DomainUser
            var user = DomainUser.Create(userId, username, email)
                ?? throw new BadRequestException("Cannot create user");

            await _userRepository.AddAsync(user);
            await _messageBroker.PublishAsync(new NewDomainUserCreated(new Guid(userId), username, user.ProfileImagePath));

            _logger.LogInformation($"User {userId} created.");


            // UserStatus
            var userStatus = UserStatus.Create(new Guid(userId))
                ?? throw new BadRequestException("Cannot create user status");

            await _userStatusRepository.AddAsync(userStatus);

            _logger.LogInformation($"UserStatus for {userId} created.");
        }
    }
}