using AppUser.Application.Exceptions;
using AppUser.Application.Services;
using AppUser.Shared.DTOs;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppUser.Application.Commands.Auth.Handlers
{
    internal class SignInHandler : IRequestHandler<SignIn, UserIdentityDto>
    {
        private readonly ILogger<SignInHandler> _logger;
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;

        public SignInHandler(ILogger<SignInHandler> logger, 
            IAuthService authService, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _logger = logger;
            _authService = authService;
        }
        public async Task<UserIdentityDto> Handle(SignIn command, CancellationToken cancellationToken)
        {
            var result = await _authService.SigninUserAsync(command.Email, command.Password);

            if(!result)
            {
                throw new BadRequestException("Cannot sign in");
            }

            var (userId, username, email, roles) = await _authService.GetUserDetailsByEmailAsync(command.Email);

            await _authService.UpdateRefreshToken(userId, _tokenService.GenerateRefreshToken());
            string token = _tokenService.GenerateAccessToken(email, username, roles);

            if(token == null)
            {
                throw new BadRequestException("Cannot create access token");
            }

            _logger.LogInformation($"AuthUser {userId} signed in");

            return new UserIdentityDto(new Guid(userId), username, token);
        }
    }
}
