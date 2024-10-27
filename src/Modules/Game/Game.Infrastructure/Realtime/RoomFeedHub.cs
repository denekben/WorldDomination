using Game.Domain.RoomAggregate.Entities;
using Game.Infrastructure.Mappers;
using Microsoft.AspNetCore.SignalR;
using Game.Application.Services;

namespace Game.Infrastructure.Realtime
{
    internal sealed class RoomFeedHub : Hub<IRoomClient>, IRoomFeedHub
    {
        public async Task CreateRoom(Room room)
        {
            var roomDto = room.AsRoomDto();
            await Clients.All.ReceiveCreateRoomMessage(roomDto);
        }

        public async Task CloseRoom(Guid roomId)
        {
            await Clients.All.ReceiveCloseRoomMessage(roomId);
        }

        public async Task UpdateRoomMembers(Room room)
        {
            var roomDto = room.AsRoomDto();
            await Clients.All.ReceiveRoomMemberUpdateMessage(roomDto);
        }
    }
}
