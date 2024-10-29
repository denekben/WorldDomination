using Identity.Application.Services;
using Identity.Shared.DTOs;
using MediatR;
using Microsoft.Extensions.Logging;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Identity.Application.Commands.Auth.Handlers
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
                throw new BadRequestException("Invalid login or password");
            }

            var (userId, username, email, roles) = await _authService.GetUserDetailsByEmailAsync(command.Email);

            var refreshToken = _tokenService.GenerateRefreshToken();

            await _authService.UpdateRefreshToken(userId, refreshToken);

            string accessToken = _tokenService.GenerateAccessToken(userId, email, username, roles)
                ?? throw new BadRequestException("Cannot create access token");

            _logger.LogInformation($"AuthUser {userId} signed in");

            return new UserIdentityDto(new Guid(userId), username, accessToken, refreshToken);
        }
    }
}
