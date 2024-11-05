using Game.Application.Helpers;
using Game.Application.Services;
using Game.Domain.DomainModels.Rooms.Entities;
using Game.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using WorldDomination.Shared.Exceptions.CustomExceptions;
using WorldDomination.Shared.Services;

namespace Game.Application.Rooms.Commands.Handlers
{
    internal sealed class LeaveRoomHandler : IRequestHandler<LeaveRoom>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomMemberRepository _memberRepository;
        private readonly IHttpContextService _contextService;
        private readonly ILogger<LeaveRoomHandler> _logger;
        private readonly IGameModuleNotificationService _notifications;
        private readonly IGameModuleHelper _gameModuleHelper;

        public LeaveRoomHandler(IRoomRepository roomRepository, IHttpContextService contextService,
            ILogger<LeaveRoomHandler> logger, IRoomMemberRepository memberRepository, IGameModuleNotificationService notifications,
            IGameModuleHelper gameModuleHelper)
        {
            _roomRepository = roomRepository;
            _contextService = contextService;
            _logger = logger;
            _memberRepository = memberRepository;
            _notifications = notifications;
            _gameModuleHelper = gameModuleHelper;
        }

        public async Task Handle(LeaveRoom command, CancellationToken cancellationToken)
        {
            var userId = _contextService.GetCurrentUserId();
            var member = await _memberRepository.GetAsync(userId, command.roomId)
                ?? throw new BadRequestException("Cannot find RoomMember");

            var room = await _roomRepository.GetAsync(command.roomId, RoomIncludes.RoomMembers)
                ?? throw new BadRequestException("Cannot find Room");

            if (member.CountryId != null)
                await _gameModuleHelper.RemoveMemberFromCountry(member);

            room.RemoveMember(member);
            await _roomRepository.UpdateAsync(room);
            _logger.LogInformation($"Member {member.GameUserId} left Room {member.RoomId}");
            await _notifications.MemberLeftRoom(member, room.Id);

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
                await _notifications.MemberPromotedToOrganizer(member, room.Id);
                return;
            }
        }
    }
}
