using Identity.Shared.IntegrationEvents;
using Microsoft.Extensions.Logging;
using Shared.Events;
using System;
using System.Threading;
using System.Threading.Tasks;
using User.Domain.Entities;
using User.Domain.Repositories;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace User.Application.Users.ExternalEvents.Handlers
{
    internal class NewUserRegisteredHandler : IEventHandler<NewUserRegistered>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<NewUserRegisteredHandler> _logger;
        private readonly IUserStatusRepository _userStatusRepository;

        public NewUserRegisteredHandler(IUserRepository userRepository, ILogger<NewUserRegisteredHandler> logger, IUserStatusRepository userStatusRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
            _userStatusRepository = userStatusRepository;
        }

        public async Task HandleAsync(NewUserRegistered @event, CancellationToken cancellationToken = default)
        {
            var (userId, username, email) = @event;

            // DomainUser
            var user = DomainUser.CreateUser(userId, username, email) 
                ?? throw new BadRequestException("Cannot create user");

            await _userRepository.AddAsync(user);

            _logger.LogInformation($"User {userId} created.");


            // UserStatus
            var userStatus = UserStatus.Create(new Guid(userId)) 
                ?? throw new BadRequestException("Cannot create user status");

            await _userStatusRepository.AddAsync(userStatus);

            _logger.LogInformation($"UserStatus for {userId} created.");
        }
    }
}
