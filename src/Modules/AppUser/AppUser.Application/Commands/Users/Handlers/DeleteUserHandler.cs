using AppUser.Application.Exceptions;
using AppUser.Application.Services;
using AppUser.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Shared.Messaging;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using System;

namespace AppUser.Application.Commands.Users.Handlers
{
    internal class DeleteUserHandler : IRequestHandler<DeleteUser>
    {
        private readonly ILogger<DeleteUserHandler> _logger;
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public DeleteUserHandler(ILogger<DeleteUserHandler> logger, IAuthService authService,
            IUserRepository userRepository)
        {
            _logger = logger;
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task Handle(DeleteUser command, CancellationToken cancellationToken)
        {
            // Identity user
            if (!await _authService.DeleteUserAsync(command.UserId))
            {
                throw new BadRequestException("Cannot delete user");
            }

            var user = await _userRepository.GetAsync(new Guid(command.UserId));
            await _userRepository.DeleteAsync(user);
            _logger.LogInformation($"User {command.UserId} deleted");

            await Task.CompletedTask;
        }
    }
}
