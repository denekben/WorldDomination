using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using User.Domain.Repositories;
using WorldDomination.Shared.Exceptions.CustomExceptions;
using WorldDomination.Shared.Services;

namespace User.Application.Users.Commands.Handlers
{
    internal class ChangeProfileInfoHandler : IRequestHandler<ChangeProfileInfo>
    {
        private readonly IHttpContextService _httpContextService;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<ChangeProfileInfoHandler> _logger;

        public ChangeProfileInfoHandler(IHttpContextService httpContextService, 
            IUserRepository userRepository, ILogger<ChangeProfileInfoHandler> logger)
        {
            _httpContextService = httpContextService;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task Handle(ChangeProfileInfo command, CancellationToken cancellationToken)
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
