using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using User.Domain.Repositories;
using Users.Application.Users.Commands;
using WorldDomination.Shared.Exceptions.CustomExceptions;
using WorldDomination.Shared.Services;

namespace User.Application.Users.Commands.Handlers
{
    internal class UpdateCurrentUserInfoHandler : IRequestHandler<UpdateCurrentUserInfo>
    {
        private readonly IHttpContextService _httpContextService;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UpdateCurrentUserInfoHandler> _logger;

        public UpdateCurrentUserInfoHandler(IHttpContextService httpContextService, 
            IUserRepository userRepository, ILogger<UpdateCurrentUserInfoHandler> logger)
        {
            _httpContextService = httpContextService;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task Handle(UpdateCurrentUserInfo command, CancellationToken cancellationToken)
        {
            var userId = _httpContextService.GetCurrentUserId();

            var user = await _userRepository.GetAsync(userId) ??
                throw new BadRequestException("Cannot find user");

            user.ChangeProfileInfo(command.Name, command.Bio);

            await _userRepository.UpdateAsync(user);

            _logger.LogInformation($"Updated profile info for {userId}");
        }
    }
}
