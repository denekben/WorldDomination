using AppUser.Application.Commands.Auth.Handlers;
using AppUser.Application.Commands.Auth;
using AppUser.Application.Exceptions;
using AppUser.Application.Services;
using AppUser.Domain.Repositories;
using AppUser.Shared.DTOs;
using AppUser.Shared.Events;
using Microsoft.Extensions.Logging;
using Shared.Messaging;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using AppUser.Application.Commands.Users;
using System;

namespace AppUser.Application.Commands.Account.Handlers
{
    internal class DeleteUserHandler : IRequestHandler<DeleteUser>
    {
        private readonly ILogger<DeleteUserHandler> _logger;
        private readonly IAuthService _authService;
        private readonly IMessageBroker _messageBroker;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public DeleteUserHandler(ILogger<DeleteUserHandler> logger, IAuthService authService,
            IMessageBroker messageBroker, ITokenService tokenService, IUserRepository userRepository)
        {
            _logger = logger;
            _authService = authService;
            _messageBroker = messageBroker;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        public async Task Handle(DeleteUser command, CancellationToken cancellationToken)
        {
            // Identity user
            if (!await _authService.DeleteUserAsync(command.UserId))
            {
                throw new BadRequestException("Cannot delete user");
            }

            await _userRepository.DeleteAsync(new Guid(command.UserId));

            await Task.CompletedTask;
        }
    }
}
