using Game.Domain.DomainModels.RoomAggregate.Abstractions;
using DomainGame = Game.Domain.GameAggregate.Entities.Game;
using Game.Domain.RoomAggregate.Entities;

namespace Game.Application.Services
{
    public interface IGameModuleNotificationService
    {
        Task RoomCreated(Room room);
        Task RoomClosed(Guid roomId);
        Task RoomUpdated(Room room);
        Task MemberLeftRoom(RoomMember member, Guid roomId);
        Task MemberJoinedRoom(RoomMember member, Guid roomId);
        Task GameCreated(DomainGame game);
    }
}
