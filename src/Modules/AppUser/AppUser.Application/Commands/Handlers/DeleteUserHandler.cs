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
    public class DeleteUserHandler : ICommandHandler<DeleteUser>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageBroker _messageBroker;
        private readonly ILogger<DeleteUserHandler> _logger;

        public DeleteUserHandler(IUserRepository userRepository, IUserService userService,
            IMessageBroker messageBroker, ILogger<DeleteUserHandler> logger)
        {
            _userRepository = userRepository;
            _messageBroker = messageBroker;
            _logger = logger;
        }

        public async Task HandleAsync(DeleteUser command, CancellationToken cancellationToken = default)
        {
            var user = _userRepository.GetAsync(command.Id);
            if (await user is null)
            {
                throw new UserNotFoundByIdException(command.Id);
            }

            var deletedUser = await _userRepository.DeleteAsync(command.Id);

            await _messageBroker.PublishAsync(new UserDeleted(deletedUser.Id), cancellationToken);

            _logger.LogInformation($"User with ID: {deletedUser.Id} was deleted.");
        }
    }
}
