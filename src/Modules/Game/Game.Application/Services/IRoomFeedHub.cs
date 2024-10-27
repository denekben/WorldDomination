using Game.Domain.RoomAggregate.Entities;

namespace Game.Application.Services
{
    public interface IRoomFeedHub
    {
        Task CreateRoom(Room room);
        Task CloseRoom(Guid roomId);
        Task UpdateRoomMembers(Room room);
    }
}
