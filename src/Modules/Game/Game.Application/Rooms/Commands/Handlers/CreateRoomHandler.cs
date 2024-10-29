using Game.Application.Services;
using Game.Domain.RoomAggregate.Entities;
using Game.Domain.UserAggregate.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using WorldDomination.Shared.Domain;
using WorldDomination.Shared.Exceptions.CustomExceptions;
using WorldDomination.Shared.Services;

namespace Game.Application.Rooms.Commands.Handlers
{
    internal sealed class CreateRoomHandler : IRequestHandler<CreateRoom, Guid>
    {
        private readonly ILogger<CreateRoomHandler> _logger;
        private readonly IRepository<Room> _roomRepository;
        private readonly IRepository<GameUser> _userRepository;
        private readonly IHttpContextService _contextService;
        private readonly IRoomNotificationService _roomNotificationService;

        public CreateRoomHandler(ILogger<CreateRoomHandler> logger,
            IRepository<Room> roomRepository, IHttpContextService contextService, 
            IRoomNotificationService roomNotificationService, IRepository<GameUser> userRepository)
        {
            _logger = logger;
            _roomRepository = roomRepository;
            _contextService = contextService;
            _roomNotificationService = roomNotificationService;
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(CreateRoom command, CancellationToken cancellationToken)
        {
            var userId = _contextService.GetCurrentUserId();
            var user = await _userRepository.GetAsync(userId)
                ?? throw new BadRequestException("Cannot create room: invalid userId");

            var(roomName, gameType, roomLimit, countryQuantity, isPublic, roomCode) = command;

            var room = Room.Create(user.Id, roomName, gameType, roomLimit, countryQuantity, isPublic, roomCode)
                ?? throw new BadRequestException("Cannot create room");

            var organizer = Organizer.Create(user.Id, room.Id, user.Name, user.ProfileImagePath)
                ?? throw new BadRequestException("Cannot create organizer");

            room.AddMember(organizer);

            await _roomRepository.AddAsync(room);
            _logger.LogInformation($"Room {room.Id} created");

            await _roomNotificationService.CreateRoom(room);

            return room.Id;
        }
    }
}
