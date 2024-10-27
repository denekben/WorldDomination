using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Messaging;
using User.Domain.Entities;
using User.Shared.IntegrationEvents;
using Users.Application.Users.Commands;
using WorldDomination.Shared.Domain;
using WorldDomination.Shared.Exceptions.CustomExceptions;
using WorldDomination.Shared.Services;

namespace User.Application.Users.Commands.Handlers
{
    internal class UpdateCurrentUserInfoHandler : IRequestHandler<UpdateCurrentUserInfo>
    {
        private readonly IHttpContextService _httpContextService;
        private readonly IRepository<DomainUser> _userRepository;
        private readonly ILogger<UpdateCurrentUserInfoHandler> _logger;
        private readonly IMessageBroker _messageBroker;

        public UpdateCurrentUserInfoHandler(IHttpContextService httpContextService,
            IRepository<DomainUser> userRepository, ILogger<UpdateCurrentUserInfoHandler> logger, IMessageBroker messageBroker)
        {
            _httpContextService = httpContextService;
            _userRepository = userRepository;
            _logger = logger;
            _messageBroker = messageBroker;
        }

        public async Task Handle(UpdateCurrentUserInfo command, CancellationToken cancellationToken)
        {
            var userId = _httpContextService.GetCurrentUserId();

            var user = await _userRepository.GetAsync(userId) ??
                throw new BadRequestException("Cannot find user");

            user.ChangeProfileInfo(command.Name ?? user.Username, command.Bio ?? string.Empty);

            await _userRepository.UpdateAsync(user);
            await _messageBroker.PublishAsync(new NameChanged(userId, command.Name ?? user.Username));

            _logger.LogInformation($"Updated profile info for {userId}");
        }
    }
}