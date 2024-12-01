using Game.Application.Services;
using Game.Application.UseCases.Rooms.Commands;
using Game.Domain.DomainModels.Rooms.Entities;
using Game.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using WorldDomination.Shared.Exceptions.CustomExceptions;
using WorldDomination.Shared.Services;

namespace Game.Application.UseCases.Rooms.Commands.Handlers
{
    internal sealed class CreateRoomHandler : IRequestHandler<CreateRoom, Guid>
    {
        private readonly ILogger<CreateRoomHandler> _logger;
        private readonly IRoomRepository _roomRepository;
        private readonly IGameModuleReadService _readService;
        private readonly IGameModuleNotificationService _notifications;

        public CreateRoomHandler(ILogger<CreateRoomHandler> logger,
            IRoomRepository roomRepository, IGameModuleNotificationService notifications,
            IGameModuleReadService readService)
        {
            _logger = logger;
            _roomRepository = roomRepository;
            _notifications = notifications;
            _readService = readService;
        }

        public async Task<Guid> Handle(CreateRoom command, CancellationToken cancellationToken)
        {
            var userId = command.CallerId;
            var user = await _readService.GetUserAsync(userId)
                ?? throw new BadRequestException("Cannot create room: invalid userId");

            var (_,roomName, gameType, hasTeams, memberLimit, roundQuantity, countryLimit, isPrivate, roomCode) = command;

            roomName = string.IsNullOrWhiteSpace(roomName) ? user.Name + "'s room" : roomName;

            var room = Room.Create(user.Id, roomName, gameType, hasTeams, memberLimit, roundQuantity, countryLimit, isPrivate, roomCode)
                ?? throw new BadRequestException("Cannot create Room");

            var organizer = Organizer.Create(user.Id, room.Id, user.Name, user.ProfileImagePath)
                ?? throw new BadRequestException("Cannot create Organizer");

            room.AddMember(organizer, room.RoomCode);

            await _roomRepository.AddAsync(room);
            _logger.LogInformation($"Room {room.Id} created");
            await _notifications.RoomCreated(room);

            return room.Id;
        }
    }
}
