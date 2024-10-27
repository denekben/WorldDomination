using User.Application.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using WorldDomination.Shared.Exceptions.CustomExceptions;
using WorldDomination.Shared.Services;
using Users.Application.Users.Commands;
using WorldDomination.Shared.Domain;
using User.Domain.Entities;

namespace User.Application.Users.Commands.Handlers
{
    internal class UpdateCurrentUserImageHandler : IRequestHandler<UpdateCurrentUserImage>
    {
        private readonly ILogger<UpdateCurrentUserImageHandler> _logger;
        private readonly IRepository<DomainUser> _userRepository;
        private readonly IProfileImageService _profileImageService;
        private readonly IHttpContextService _httpContextService;

        public UpdateCurrentUserImageHandler(ILogger<UpdateCurrentUserImageHandler> logger, IProfileImageService profileImageService,
             IRepository<DomainUser> userRepository, IHttpContextService httpContextService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _profileImageService = profileImageService;
            _httpContextService = httpContextService;
        }

        public async Task Handle(UpdateCurrentUserImage command, CancellationToken cancellationToken)
        {
            var userId = _httpContextService.GetCurrentUserId();

            var user = await _userRepository.GetAsync(userId)
                ?? throw new BadRequestException("Cannot find user");

            var imagePath = await _profileImageService.UploadFileAsync(command.FormFile);
            if (string.IsNullOrEmpty(imagePath))
            {
                throw new BadRequestException("Cannot upload image");
            }

            user.ChangeProfileImagePath(imagePath);

            await _userRepository.UpdateAsync(user);
            _logger.LogInformation($"User {userId} changed profile image");
        }
    }
}