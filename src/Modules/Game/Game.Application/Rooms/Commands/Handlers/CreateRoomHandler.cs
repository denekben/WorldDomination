using Game.Domain.RoomAggregate.Entities;
using Game.Domain.UserAggregate.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using WorldDomination.Shared.Domain;

namespace Game.Application.Rooms.Commands.Handlers
{
    internal sealed class CreateRoomHandler : IRequestHandler<CreateRoom>
    {
        private readonly IRepository<GameUser> _userRepository;
        private readonly ILogger<CreateRoomHandler> _logger;
        private readonly IRepository<Room> _roomRepository;

        public async Task Handle(CreateRoom command, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}
