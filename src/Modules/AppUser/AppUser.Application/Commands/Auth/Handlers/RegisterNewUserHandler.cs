using AppUser.Application.Exceptions;
using AppUser.Application.Services;
using AppUser.Domain.Repositories;
using AppUser.Shared.DTOs;
using AppUser.Shared.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Messaging;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;

namespace AppUser.Application.Commands.Auth.Handlers
{
    public class RegisterNewUserHandler : IRequestHandler<RegisterNewUser, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageBroker _messageBroker;
        private readonly ILogger<RegisterNewUserHandler> _logger;
        private readonly IAuthService _authService;

        public RegisterNewUserHandler(IUserRepository userRepository,
            IMessageBroker messageBroker, ILogger<RegisterNewUserHandler> logger, IAuthService authService)
        {
            _userRepository = userRepository;
            _messageBroker = messageBroker;
            _logger = logger;
            _authService = authService;
        }

        public async Task<UserDto> Handle(RegisterNewUser command, CancellationToken cancellationToken)
        {
            var (result, userId) = await _authService.CreateUserAsync(command.Username, command.Password, command.Email);
            if (!result)
            {
                throw new BadRequestException("Cannot create user");
            }

            await _messageBroker.PublishAsync(new NewUserRegistered(userId, command.Username, command.Email), cancellationToken);

            _logger.LogInformation($"AuthUser with ID: {userId} registered");
        }
    }
}
