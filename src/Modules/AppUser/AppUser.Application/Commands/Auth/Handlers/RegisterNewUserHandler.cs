using AppUser.Application.Exceptions;
using AppUser.Application.Services;
using AppUser.Domain.Entities;
using AppUser.Domain.Repositories;
using AppUser.Shared.DTOs;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserAccess.Domain.Entities;

namespace AppUser.Application.Commands.Auth.Handlers
{
    internal class RegisterNewUserHandler : IRequestHandler<RegisterNewUser, UserIdentityDto>
    {
        private readonly ILogger<RegisterNewUserHandler> _logger;
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        private readonly IActivityStatusRepository _activityStatusRepository;

        public RegisterNewUserHandler(ILogger<RegisterNewUserHandler> logger, IAuthService authService,
            ITokenService tokenService, IUserRepository userRepository, IActivityStatusRepository activityStatusRepository)
        {
            _tokenService = tokenService;
            _logger = logger;
            _authService = authService;
            _userRepository = userRepository;
            _activityStatusRepository = activityStatusRepository;
        }

        public async Task<UserIdentityDto> Handle(RegisterNewUser command, CancellationToken cancellationToken)
        {
            // Identity User
            var (username, password, email) = command;

            var (result, userId) = await _authService.CreateUserAsync(username, password, email);
            if (!result)
            {
                throw new BadRequestException("Cannot create user");
            }
            _logger.LogInformation($"AuthUser {userId} registered");

            await _authService.AssignUserToRole(email, "Member");
            if(!result)
            {
                throw new BadRequestException("Cannot assign user to role");
            }
            _logger.LogInformation($"AuthUser {userId} assigned to role Member");

            await _authService.UpdateRefreshToken(userId, _tokenService.GenerateRefreshToken());
            string token = _tokenService.GenerateAccessToken(email, username, await _authService.GetUserRolesAsync(userId));
            if(token == null)
            {
                throw new BadRequestException("Cannot generate access token");
            }

            // Domain User
            var user = User.CreateUser(userId, username, email);
            if (user == null)
            {
                throw new BadRequestException("Cannot create user");
            }
            _logger.LogInformation($"User {userId} created.");


            // ActivityStatus
            var activityStatus = ActivityStatus.Create(new Guid(userId));
            if (activityStatus == null)
            {
                throw new BadRequestException("Cannot create activity status");
            }
            _logger.LogInformation($"ActivityStatus for {userId} created.");
            await _activityStatusRepository.AddAsync(activityStatus);

            await _userRepository.AddAsync(user);

            return new UserIdentityDto(new Guid(userId), username, token);
        }
    }
}
