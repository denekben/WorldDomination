using Game.Shared.DTOs;

namespace Game.Infrastructure.Realtime
{
    public interface IGameModuleHubClient
    {
        Task ReceiveRoomCreatedMessage(RoomDto roomDto);
        Task ReceiveRoomClosedMessage(Guid roomId);
        Task ReceiveRoomUpdatedMessage(RoomDto roomDto);
        Task ReceiveMemberLeftRoomMessage(RoomMemberDto memberDto, Guid roomId);
        Task ReceiveMemberJoinedRoomMessage(RoomMemberDto memberDto, Guid roomId);
    }
}
