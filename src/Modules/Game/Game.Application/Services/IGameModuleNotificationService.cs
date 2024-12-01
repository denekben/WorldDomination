using Game.Domain.DomainModels.Rooms.Entities;
using Game.Domain.DomainModels.Games.Entities;
using Game.Application.DTOs;

namespace Game.Application.Services
{
    public interface IGameModuleNotificationService
    {
        Task RoomCreated(Room room);
        Task RoomClosedForAll(Guid roomId);
        Task RoomClosedForRoom(Guid roomId);
        Task MemberJoinedRoomForAll(RoomMember member);
        Task MemberJoinedRoomForRoom(RoomMember member);
        Task MemberLeftRoomForAll(Guid roomId, Guid memberId);
        Task MemberLeftRoomForRoom(Guid roomId, Guid memberId);
        Task GameCreatedForAll(Guid roomId);
        Task GameCreatedForRoom(Guid roomId);
        Task MemberPromotedToOrganizer(Guid memberId, Guid roomId);
        Task CountryCreated(Country country, Guid roomId);
        Task MemberJoinedCountry(Guid memberId, Guid roomId, Guid countryId);
        Task MinisterPromotedToPresident(Guid memberId, Guid roomId, Guid countryId);
        Task MemberLeftCountry(Guid memberId, Guid roomId, Guid countryId);
        Task OrderSent(Guid countryId, Guid roomId);
        Task GameStateChanged(string gameState, Guid roomId);
        Task GameEnded(Guid roomId);
        Task OrderChanged(OrderDto orderDto, string callerId);
    }
}
