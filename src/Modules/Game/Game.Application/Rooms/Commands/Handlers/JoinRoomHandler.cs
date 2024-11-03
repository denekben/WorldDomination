using Game.Application.Services;
using Game.Domain.DomainModels.RoomAggregate.Entities;
using Game.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using WorldDomination.Shared.Exceptions.CustomExceptions;
using WorldDomination.Shared.Services;

namespace Game.Application.Rooms.Commands.Handlers
{
    internal sealed class JoinRoomHandler : IRequestHandler<JoinRoom, Guid>
    {
        private readonly IHttpContextService _contextService;
        private readonly IUserRepository _userRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly ILogger<JoinRoomHandler> _logger;
        private readonly IGameModuleNotificationService _notifications;

        public JoinRoomHandler(IHttpContextService contextService, IUserRepository userRepository,
            IRoomRepository roomRepository, ILogger<JoinRoomHandler> logger, IGameModuleNotificationService notifications)
        {
            _contextService = contextService;
            _userRepository = userRepository;
            _roomRepository = roomRepository;
            _logger = logger;
            _notifications = notifications;
        }

        public async Task<Guid> Handle(JoinRoom command, CancellationToken cancellationToken)
        {
            var userId = _contextService.GetCurrentUserId();
            var user = await _userRepository.GetAsync(userId)
                ?? throw new BadRequestException("Cannot join Room: invalid userId");

            var room = await _roomRepository.GetAsync(command.RoomId, RoomIncludes.DomainGame)
                ?? throw new BadRequestException("Cannot find room");

            if (room.DomainGame != null)
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
