using AppUser.Application.Exceptions;
using AppUser.Application.Services;
using AppUser.Domain.Repositories;
using AppUser.Shared.Events;
using Microsoft.Extensions.Logging;
using Shared.Commands;
using Shared.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace AppUser.Application.Commands.Handlers
{
    internal class ChangeUsernameHandler : ICommandHandler<ChangeUsername>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageBroker _messageBroker;
        private readonly ILogger<ChangeUsernameHandler> _logger;

        public ChangeUsernameHandler(IUserRepository userRepository, IUserService userService,
            IMessageBroker messageBroker, ILogger<ChangeUsernameHandler> logger)
        {
            _userRepository = userRepository;
            _messageBroker = messageBroker;
            _logger = logger;
        }

        public async Task HandleAsync(ChangeUsername command, CancellationToken cancellationToken = default)
        {
            var (id, username) = command;

            var user = await _userRepository.GetAsync(id);

            if (user is null)
            {
                throw new UserNotFoundByIdException(id);
            }

            var updatedUser = await _userRepository.UpdateAsync(user);

            await _messageBroker.PublishAsync(new UsernameChanged(updatedUser.Id, updatedUser.Username), cancellationToken);

            _logger.LogInformation($"User with ID: {id} changed name \'{user.Username}\' to \'{updatedUser.Username}\'");
        }
    }
}
