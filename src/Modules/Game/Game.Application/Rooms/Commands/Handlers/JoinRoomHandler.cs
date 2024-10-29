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
    internal sealed class JoinRoomHandler : IRequestHandler<JoinRoom>
    {
        private readonly IHttpContextService _contextService;
        private readonly IRepository<GameUser> _userRepository;
        private readonly IRepository<Room> _roomRepository;
        private readonly ILogger<JoinRoomHandler> _logger;
        private readonly IRoomNotificationService _roomNotificationService;
        private readonly IGameService _gameService;

        public JoinRoomHandler(IHttpContextService contextService, IRepository<GameUser> userRepository,
            IRepository<Room> roomRepository, ILogger<JoinRoomHandler> logger, IRoomNotificationService roomNotificationService, IGameService gameService)
        {
            _contextService = contextService;
            _userRepository = userRepository;
            _roomRepository = roomRepository;
            _logger = logger;
            _roomNotificationService = roomNotificationService;
            _gameService = gameService;
        }

        public async Task Handle(JoinRoom command, CancellationToken cancellationToken)
        {
            var userId = _contextService.GetCurrentUserId();
            var user = await _userRepository.GetAsync(userId)
                ?? throw new BadRequestException("Cannot join Room: invalid userId");

            var room = await _roomRepository.GetAsync(command.RoomId)
                ?? throw new BadRequestException("Cannot find room");

            if (await _gameService.GetGameByRoomId(room.Id) != null)
                throw new BadRequestException("Cannot add user to Room with active Game");

            var player = Player.Create(user.Id, command.RoomId, user.Name, user.ProfileImagePath)
                ?? throw new BadImageFormatException("Cannot create Player");

            room.AddMember(player, command.RoomCode);

            await _roomRepository.UpdateAsync(room);

            await _roomNotificationService.UpdateRoom(room);
            await _roomNotificationService.JoinRoom(player, room.Id);
            _logger.LogInformation($"Added Player {player.GameUserId} to Room {command.RoomId}");
        }
    }
}
