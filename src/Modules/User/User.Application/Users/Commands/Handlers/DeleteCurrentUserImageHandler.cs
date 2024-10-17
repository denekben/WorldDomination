using MediatR;
using System.Threading;
using System.Threading.Tasks;
using User.Domain.Repositories;
using WorldDomination.Shared.Exceptions.CustomExceptions;
using Microsoft.Extensions.Logging;
using User.Application.Services;
using WorldDomination.Shared.Services;
using Users.Application.Users.Commands;

namespace User.Application.Users.Commands.Handlers
{
    internal class DeleteProfileImageHandler : IRequestHandler<DeleteCurrentUserImage>
    {
        private readonly ILogger<DeleteProfileImageHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IProfileImageService _profileImageService;
        private readonly IHttpContextService _httpContextService;

        public DeleteProfileImageHandler(ILogger<DeleteProfileImageHandler> logger, IProfileImageService profileImageService,
             IUserRepository userRepository, IHttpContextService httpContextService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _profileImageService = profileImageService;
            _httpContextService = httpContextService;
        }

        public async Task Handle(DeleteCurrentUserImage command, CancellationToken cancellationToken)
        {
            var userId = _httpContextService.GetCurrentUserId();

            var user = await _userRepository.GetAsync(userId)
                ?? throw new BadRequestException("Cannot find user");

            user.ChangeProfileImagePath();

            await _userRepository.UpdateAsync(user);
            _logger.LogInformation($"User {userId} removed profile image");
            
        }
    }
}
