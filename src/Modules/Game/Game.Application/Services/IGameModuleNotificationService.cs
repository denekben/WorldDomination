using DomainGame = Game.Domain.DomainModels.GameAggregate.Entities.Game;
using Game.Domain.DomainModels.RoomAggregate.Entities;
using Game.Domain.DomainModels.GameAggregate.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Diagnostics.Metrics;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
    }
}
