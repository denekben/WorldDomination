using AppUser.Application.Exceptions;
using AppUser.Application.Services;
using AppUser.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AppUser.Application.Commands.Users.Handlers
{
    internal class ChangeProfileImageHandler : IRequestHandler<ChangeProfileImage>
    {
        private readonly ILogger<ChangeProfileImageHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IProfileImageService _profileImageService;

        public ChangeProfileImageHandler(ILogger<ChangeProfileImageHandler> logger, IProfileImageService profileImageService,
             IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _profileImageService = profileImageService;
        }
        public async Task Handle(ChangeProfileImage command, CancellationToken cancellationToken)
        {
            var imagePath = await _profileImageService.UploadFileAsync(command.FormFile);
            if (string.IsNullOrEmpty(imagePath))
            {
                throw new BadRequestException("Cannot upload image");
            }
            var user = await _userRepository.GetAsync(command.UserId);
            user.ChangeProfileImagePath(imagePath);

            await _userRepository.UpdateAsync(user);
            _logger.LogInformation($"User {command.UserId} changed profile image");

            await Task.CompletedTask;
        }
    }
}
