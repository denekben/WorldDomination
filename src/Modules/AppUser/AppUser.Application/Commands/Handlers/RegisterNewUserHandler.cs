using AppUser.Application.Exceptions;
using AppUser.Application.Services;
using AppUser.Domain.Repositories;
using AppUser.Shared.Events;
using Microsoft.Extensions.Logging;
using Shared.Commands;
using Shared.Messaging;
using System.Threading;
using System.Threading.Tasks;
using UserAccess.Domain.Entities;

namespace AppUser.Application.Commands.Handlers
{
    public class RegisterNewUserHandler : ICommandHandler<RegisterNewUser>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IMessageBroker _messageBroker;
        private readonly ILogger<RegisterNewUserHandler> _logger;

        public RegisterNewUserHandler(IUserRepository userRepository, IUserService userService, 
            IMessageBroker messageBroker, ILogger<RegisterNewUserHandler> logger)
        {
            _userRepository = userRepository;
            _userService = userService;
            _messageBroker = messageBroker;
            _logger = logger;
        }

        public async Task HandleAsync(RegisterNewUser command, CancellationToken cancellationToken = default)
        {
            if(await _userService.ExistsByNameAsync(command.Username))
            {
                throw new UserNameAlreadyExistsException(command.Username);
            }

            var newUser = await _userRepository.AddAsync(User.CreateMember(command.Username));

            await _messageBroker.PublishAsync(new NewUserRegistered(newUser.Id, newUser.Username), cancellationToken);

            _logger.LogInformation($"New user registered with ID: {newUser.Id}.");
        }
    }
}
