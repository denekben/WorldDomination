using User.Application.Services;
using User.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using WorldDomination.Shared.Exceptions.CustomExceptions;
using System;

namespace User.Application.Users.Commands.Handlers
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
            var user = await _userRepository.GetAsync(new Guid(command.UserId)) 
                ?? throw new BadRequestException("Cannot find user");

            var imagePath = await _profileImageService.UploadFileAsync(command.FormFile);
            if (string.IsNullOrEmpty(imagePath))
            {
                throw new BadRequestException("Cannot upload image");
            }

            user.ChangeProfileImagePath(imagePath);

            await _userRepository.UpdateAsync(user);
            _logger.LogInformation($"User {command.UserId} changed profile image");
        }
    }
}
