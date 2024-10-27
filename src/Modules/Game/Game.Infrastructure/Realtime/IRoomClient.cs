using Game.Shared.DTOs;

namespace Game.Infrastructure.Realtime
{
    internal interface IRoomClient
    {
        Task ReceiveCreateRoomMessage(RoomDto roomDto);
        Task ReceiveCloseRoomMessage(Guid roomId);
        Task ReceiveRoomMemberUpdateMessage(RoomDto roomDto);
    }
}
