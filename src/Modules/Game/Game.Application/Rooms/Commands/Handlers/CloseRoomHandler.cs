using Game.Application.Services;
using Game.Domain.DomainModels.RoomAggregate.Abstractions;
using Game.Domain.RoomAggregate.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using WorldDomination.Shared.Domain;
using WorldDomination.Shared.Exceptions.CustomExceptions;
using WorldDomination.Shared.Services;

namespace Game.Application.Rooms.Commands.Handlers
{
    internal sealed class CloseRoomHandler : IRequestHandler<CloseRoom>
    {
        private readonly IHttpContextService _contextService;
        private readonly IRepository<Room> _roomRepository;
        private readonly IRepository<RoomMember> _roomMemberRepository;
        private readonly ILogger<CloseRoomHandler> _logger;
        private readonly IGameModuleService _gameService;
        private readonly IGameModuleNotificationService _notifications;

        public CloseRoomHandler(IHttpContextService contextService, IRepository<Room> roomRepository,
            IRepository<RoomMember> roomMemberRepository, ILogger<CloseRoomHandler> logger, IGameModuleService gameService, IGameModuleNotificationService notifications)
        {
            _contextService = contextService;
            _roomRepository = roomRepository;
            _roomMemberRepository = roomMemberRepository;
            _logger = logger;
            _gameService = gameService;
            _notifications = notifications;
        }

        public async Task Handle(CloseRoom command, CancellationToken cancellationToken)
        {
            var userId = _contextService.GetCurrentUserId();
            var member = await _roomMemberRepository.GetAsync(userId)
                ?? throw new BadRequestException("Room member not found");

            var room = await _roomRepository.GetAsync(command.roomId)
                ?? throw new BadRequestException("Room not found");

            if (member is not Organizer)
                throw new BadRequestException("Only RoomCreator can close Room");

            if (room.DomainGame != null)
                throw new BadRequestException($"Cannot delete Room {room.Id} with existing Game");

            await _roomRepository.DeleteAsync(room);
            await _notifications.RoomClosed(room.Id);
            _logger.LogInformation($"Room {room.Id} deleted");
        }
    }
}