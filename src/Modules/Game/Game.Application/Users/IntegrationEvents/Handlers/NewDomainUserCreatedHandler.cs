﻿using Game.Domain.UserAggregate.Entities;
using Microsoft.Extensions.Logging;
using Shared.Events;
using User.Shared.IntegrationEvents;
using WorldDomination.Shared.Domain;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Application.Users.IntegrationEvents.Handlers
{
    internal sealed class NewDomainUserCreatedHandler : IEventHandler<NewDomainUserCreated>
    {
        private readonly IRepository<GameUser> _userRepository;
        private readonly ILogger<NewDomainUserCreatedHandler> _logger;

        public NewDomainUserCreatedHandler(IRepository<GameUser> userRepository, ILogger<NewDomainUserCreatedHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task HandleAsync(NewDomainUserCreated @event, CancellationToken cancellationToken = default)
        {
            var user = GameUser.Create(@event.UserId, @event.Username, @event.ProfileImagePath)
                ?? throw new BadRequestException($"Cannot create GameUser {@event.UserId}");

            await _userRepository.AddAsync(user);
            _logger.LogInformation($"GameUser {@event.UserId} created");
        }
    }
}