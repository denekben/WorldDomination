using AppUser.Application.Exceptions;
using AppUser.Application.Services;
using AppUser.Domain.Repositories;
using AppUser.Shared.DTOs;
using AppUser.Shared.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Messaging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppUser.Application.Commands.Auth.Handlers
{
    internal class ChangeUsernameHandler : IRequestHandler<ChangeUsername, UserIdentityDto>
    {
        private readonly ILogger<ChangeUsernameHandler> _logger;
        private readonly IAuthService _authService;
        private readonly IMessageBroker _messageBroker;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public ChangeUsernameHandler(ILogger<ChangeUsernameHandler> logger, IAuthService authService,
            IMessageBroker messageBroker, ITokenService tokenService, IUserRepository userRepository)
        {
            _logger = logger;
            _authService = authService;
            _messageBroker = messageBroker;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        public async Task<UserIdentityDto> Handle(ChangeUsername command, CancellationToken cancellationToken)
        {
            // Identity user
            var (userId, username) = command;

            if(!await _authService.UpdateUserName(userId.ToString(), username))
            {
                throw new BadRequestException("Cannot change username");
            }

            _logger.LogInformation($"AuthUser {userId} changed username to {username}");

            var (_, _, email, roles) = await _authService.GetUserDetailsAsync(userId.ToString());

            var token =  _tokenService.GenerateAccessToken(email, username, roles);
            if (token == null)
            {
                throw new BadRequestException("Cannot generate access token");
            }

            await _messageBroker.PublishAsync(new UsernameChangedEvent(command.UserId.ToString(), command.Username));


            // Domain user
            var user = await _userRepository.GetAsync(userId);
            if (user == null)
            {
                throw new BadRequestException($"Cannot find user {userId}");
            }
            user.ChangeUsername(username);

            await _userRepository.UpdateAsync(user);

            return new UserIdentityDto(userId, username, token);
        }
    }
}
