using Game.Shared.DTOs;

namespace Game.Infrastructure.Realtime
{
    public interface IRoomHubClient
    {
        Task ReceiveCreatedRoomMessage(RoomDto roomDto);
        Task ReceiveClosedRoomMessage(Guid roomId);
        Task ReceiveUpdatedRoomMessage(RoomDto roomDto);
        Task ReceiveLeavedRoomMessage(RoomMemberDto memberDto, Guid roomId);
        Task ReceiveJoinedRoomMessage(RoomMemberDto memberDto, Guid roomId);
    }
}
