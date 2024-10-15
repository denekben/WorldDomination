using User.Application.Services;
using User.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using WorldDomination.Shared.Exceptions.CustomExceptions;
using System;
using WorldDomination.Shared.Services;

namespace User.Application.Users.Commands.Handlers
{
    internal class ChangeProfileImageHandler : IRequestHandler<ChangeProfileImage>
    {
        private readonly ILogger<ChangeProfileImageHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IProfileImageService _profileImageService;
        private readonly IHttpContextService _httpContextService;

        public ChangeProfileImageHandler(ILogger<ChangeProfileImageHandler> logger, IProfileImageService profileImageService,
             IUserRepository userRepository, IHttpContextService httpContextService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _profileImageService = profileImageService;
            _httpContextService = httpContextService;
        }

        public async Task Handle(ChangeProfileImage command, CancellationToken cancellationToken)
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
