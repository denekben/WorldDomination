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
    internal sealed class LeaveRoomHandler : IRequestHandler<LeaveRoom>
    {
        private readonly IRepository<Room> _roomRepository;
        private readonly IRepository<RoomMember> _memberRepository;
        private readonly IHttpContextService _contextService;
        private readonly ILogger<LeaveRoomHandler> _logger;
        private readonly IGameModuleNotificationService _notifications;

        public LeaveRoomHandler(IRepository<Room> roomRepository, IHttpContextService contextService,
            ILogger<LeaveRoomHandler> logger, IRepository<RoomMember> memberRepository, IGameModuleNotificationService notifications)
        {
            _roomRepository = roomRepository;
            _contextService = contextService;
            _logger = logger;
            _memberRepository = memberRepository;
            _notifications = notifications;
        }

        public async Task Handle(LeaveRoom command, CancellationToken cancellationToken)
        {
            var userId = _contextService.GetCurrentUserId();
            var member = await _memberRepository.GetAsync(userId)
                ?? throw new BadRequestException("Cannot leave Room: invalid userId");

            var room = await _roomRepository.GetAsync(command.roomId)
                ?? throw new BadRequestException("Cannot find room");

            room.RemoveMember(member);
            await _roomRepository.UpdateAsync(room);
            await _notifications.RoomUpdated(room);
            await _notifications.MemberLeftRoom(member, room.Id);
            _logger.LogInformation($"Member {member.GameUserId} left room {member.RoomId}");

            if (room.RoomMembers.Count() == 0)
            {
                await _roomRepository.DeleteAsync(room);
                await _notifications.RoomClosed(room.Id);
                _logger.LogInformation($"Room {room.Id} deleted");
                return;
            }
            else if(member is Organizer)
            {
                var newOrganizer = room.ElectNewOrganizer(room.RoomCode);
                await _roomRepository.UpdateAsync(room);
                _logger.LogInformation($"Player {newOrganizer.GameUserId} promoted to Organizer");
                return;
            }
        }
    }
}
