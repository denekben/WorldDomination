using Game.Application.Services;
using Game.Domain.DomainModels.RoomAggregate.Abstractions;
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
        private readonly IGameModuleNotificationService _notifications;

        public CreateRoomHandler(ILogger<CreateRoomHandler> logger,
            IRepository<Room> roomRepository, IHttpContextService contextService, 
            IGameModuleNotificationService notifications, IRepository<GameUser> userRepository)
        {
            _logger = logger;
            _roomRepository = roomRepository;
            _contextService = contextService;
            _notifications = notifications;
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(CreateRoom command, CancellationToken cancellationToken)
        {
            var userId = _contextService.GetCurrentUserId();
            var user = await _userRepository.GetAsync(userId)
                ?? throw new BadRequestException("Cannot create room: invalid userId");

            var(roomName, gameType, roomLimit, countryQuantity, isPrivate, roomCode) = command;

            roomName = string.IsNullOrWhiteSpace(roomName) ? user.Name + "'s room" : roomName;

            var room = Room.Create(user.Id, roomName, gameType, roomLimit, countryQuantity, isPrivate, roomCode)
                ?? throw new BadRequestException("Cannot create room");

            var organizer = Organizer.Create(user.Id, room.Id, user.Name, user.ProfileImagePath)
                ?? throw new BadRequestException("Cannot create organizer");

            room.AddMember(organizer, room.RoomCode);

            await _roomRepository.AddAsync(room);
            _logger.LogInformation($"Room {room.Id} created");

            await _notifications.RoomCreated(room);

            return room.Id;
        }
    }
}
