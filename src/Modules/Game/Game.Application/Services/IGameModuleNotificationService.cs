using DomainGame = Game.Domain.DomainModels.Games.Entities.Game;
using Game.Domain.DomainModels.Rooms.Entities;
using Game.Domain.DomainModels.Games.Entities;

namespace Game.Application.Services
{
    public interface IGameModuleNotificationService
    {
        Task RoomCreated(Room room);
        Task RoomClosed(Guid roomId);
        Task MemberLeftRoom(RoomMember member, Guid roomId);
        Task MemberJoinedRoom(RoomMember member, Guid roomId);
        Task MemberPromotedToOrganizer(RoomMember member, Guid roomId);

        Task GameCreated(DomainGame game, Guid roomId);

        Task CountryCreated(Country country, Guid roomId);
        Task MemberJoinedCountry(RoomMember member, Guid roomId, Guid countryId);
        Task MinisterPromotedToPresident(RoomMember member, Guid roomId, Guid countryId);
        Task MemberLeftCountry(RoomMember member, Guid roomId, Guid countryId);

        Task OrderSent(Guid countryId, Guid roomId);
    }
}
