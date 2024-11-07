using Game.Application.Services;
using Game.Domain.DomainModels.Rooms.Entities;
using Game.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using WorldDomination.Shared.Exceptions.CustomExceptions;
using WorldDomination.Shared.Services;

namespace Game.Application.Rooms.Commands.Handlers
{
    internal sealed class JoinRoomHandler : IRequestHandler<JoinRoom, Guid>
    {
        private readonly IHttpContextService _contextService;
        private readonly IGameModuleReadService _readService;
        private readonly IRoomRepository _roomRepository;
        private readonly ILogger<JoinRoomHandler> _logger;
        private readonly IGameModuleNotificationService _notifications;

        public JoinRoomHandler(IHttpContextService contextService, IGameModuleReadService readService,
            IRoomRepository roomRepository, ILogger<JoinRoomHandler> logger, IGameModuleNotificationService notifications)
        {
            _contextService = contextService;
            _readService = readService;
            _roomRepository = roomRepository;
            _logger = logger;
            _notifications = notifications;
        }

        public async Task<Guid> Handle(JoinRoom command, CancellationToken cancellationToken)
        {
            var userId = _contextService.GetCurrentUserId();
            var user = await _readService.GetUserAsync(userId)
                ?? throw new BadRequestException("Cannot join Room: invalid userId");

            var room = await _roomRepository.GetAsync(command.RoomId)
                ?? throw new BadRequestException("Cannot find room");

            if (room.IsGameActive)
                throw new BadRequestException("Cannot add user to Room with active Game");

            var player = Player.Create(user.Id, command.RoomId, user.Name, user.ProfileImagePath)
                ?? throw new BadRequestException("Cannot create Player");

            room.AddMember(player, command.RoomCode);

            await _roomRepository.UpdateAsync(room);

            _logger.LogInformation($"Added Player {player.GameUserId} to Room {command.RoomId}");
            await _notifications.MemberJoinedRoom(player, room.Id);

            return room.Id;
        }
    }
}
