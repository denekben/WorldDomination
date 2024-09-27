using AppUser.Application.Exceptions;
using AppUser.Domain.Repositories;
using AppUser.Shared.Events;
using Microsoft.Extensions.Logging;
using Shared.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppUser.Application.EventHandlers
{
    internal class UsernameChangedEventHandler : IEventHandler<UsernameChangedEvent>
    {
        private readonly ILogger<UsernameChangedEventHandler> _logger;
        private readonly IUserRepository _userRepository;

        public UsernameChangedEventHandler(ILogger<UsernameChangedEventHandler> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task HandleAsync(UsernameChangedEvent @event, CancellationToken cancellationToken = default)
        {
            var (userId, username) = @event;
            var user = await _userRepository.GetAsync(new Guid(userId));
            if (user == null)
            {
                throw new BadRequestException($"Cannot find user {userId}");
            }

            user.ChangeUsername(username);

            await _userRepository.UpdateAsync(user);
        }
    }
}
