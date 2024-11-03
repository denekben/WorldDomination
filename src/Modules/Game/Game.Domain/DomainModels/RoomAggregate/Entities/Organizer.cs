using Game.Domain.DomainModels.GameAggregate.Entities;
using Game.Domain.DomainModels.RoomAggregate.Entities;
using Game.Domain.DomainModels.RoomAggregate.ValueObjects;
using Game.Domain.DomainModels.UserAggregate.Entities;
using WorldDomination.Shared.Domain;
using Game.Domain.DomainModels.RoomAggregate.Entities;

namespace Game.Domain.DomainModels.RoomAggregate.Entities
{
    public sealed class Organizer : RoomMember
    {
        //EF
        private Organizer() {}

        private Organizer(Guid creatorId, Guid gameRoomId, string name, string path)
            : base(creatorId, gameRoomId, name, path) { }

        private Organizer(Guid gameUserId, Guid roomId, string name, string path, GameRole gameRole, Guid countryId)
            : base(gameUserId, roomId, name, path, gameRole, countryId) { }

        public static Organizer Create(Guid creatorId, Guid gameRoomId, string name, string path)
        {
            return new Organizer(creatorId, gameRoomId, name, path);
        }

        public static Organizer PromoteToOrganizer(RoomMember player)
        {
            return new Organizer(player.GameUserId, player.RoomId, player.Name, player.ProfileImagePath, player.GameRole, player.CountryId);
        }
    }
}