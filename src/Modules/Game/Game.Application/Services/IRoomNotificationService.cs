using Game.Domain.DomainModels.RoomAggregate.Abstractions;
using Game.Domain.RoomAggregate.Entities;

namespace Game.Application.Services
{
    public interface IRoomNotificationService
    {
        Task CreateRoom(Room room);
        Task CloseRoom(Guid roomId);
        Task UpdateRoom(Room room);
        Task LeaveRoom(RoomMember member, Guid roomId);
        Task JoinRoom(RoomMember member, Guid roomId);
    }
}
