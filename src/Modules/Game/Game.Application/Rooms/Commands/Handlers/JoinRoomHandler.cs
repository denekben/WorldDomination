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
    internal sealed class JoinRoomHandler : IRequestHandler<JoinRoom, Guid>
    {
        private readonly IHttpContextService _contextService;
        private readonly IRepository<GameUser> _userRepository;
        private readonly IRepository<Room> _roomRepository;
        private readonly ILogger<JoinRoomHandler> _logger;
        private readonly IGameModuleNotificationService _notifications;
        private readonly IGameModuleService _gameService;

        public JoinRoomHandler(IHttpContextService contextService, IRepository<GameUser> userRepository,
            IRepository<Room> roomRepository, ILogger<JoinRoomHandler> logger, IGameModuleNotificationService notifications, IGameModuleService gameService)
        {
            _contextService = contextService;
            _userRepository = userRepository;
            _roomRepository = roomRepository;
            _logger = logger;
            _notifications = notifications;
            _gameService = gameService;
        }

        public async Task<Guid> Handle(JoinRoom command, CancellationToken cancellationToken)
        {
            var userId = _contextService.GetCurrentUserId();
            var user = await _userRepository.GetAsync(userId)
                ?? throw new BadRequestException("Cannot join Room: invalid userId");

            var room = await _roomRepository.GetAsync(command.RoomId)
                ?? throw new BadRequestException("Cannot find room");

            if (room.DomainGame != null)
                throw new BadRequestException("Cannot add user to Room with active Game");

            var player = Player.Create(user.Id, command.RoomId, user.Name, user.ProfileImagePath)
                ?? throw new BadImageFormatException("Cannot create Player");

            room.AddMember(player, command.RoomCode);

            await _roomRepository.UpdateAsync(room);

            await _notifications.RoomUpdated(room);
            await _notifications.MemberJoinedRoom(player, room.Id);
            _logger.LogInformation($"Added Player {player.GameUserId} to Room {command.RoomId}");

            return room.Id;
        }
    }
}
