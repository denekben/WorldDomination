using Game.Application.Services;
using Game.Domain.DomainModels.RoomAggregate.Abstractions;
using Game.Domain.RoomAggregate.Entities;
using Game.Infrastructure.Mappers;
using Microsoft.AspNetCore.SignalR;

namespace Game.Infrastructure.Realtime
{
    internal class RoomNotificationService : IRoomNotificationService
    {
        private readonly IHubContext<RoomHub, IRoomHubClient> _hubContext;

        public RoomNotificationService(IHubContext<RoomHub, IRoomHubClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task CreateRoom(Room room)
        {
            var roomDto = room.AsRoomDto();
            await _hubContext.Clients.All.ReceiveCreatedRoomMessage(roomDto);
        }

        public async Task CloseRoom(Guid roomId)
        {
            await _hubContext.Clients.All.ReceiveClosedRoomMessage(roomId);
        }

        public async Task UpdateRoom(Room room)
        {
            var roomDto = room.AsRoomDto();
            await _hubContext.Clients.All.ReceiveUpdatedRoomMessage(roomDto);
        }

        public async Task JoinRoom(RoomMember member, Guid roomId)
        {
            var memberDto = member.AsRoomMemberDto();
            //await _hubContext.Clients.Group()
        }

        public async Task LeaveRoom(RoomMember member, Guid roomId)
        {
            var memberDto = member.AsRoomMemberDto();
            //await _hubContext.Clients.Group()
        }
    }
}
