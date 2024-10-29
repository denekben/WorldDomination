using Game.Domain.CountryAggregate.Entities;
using Game.Domain.DomainModels.RoomAggregate.Abstractions;
using Game.Domain.RoomAggregate.Entities;
using Game.Domain.RoomAggregate.ValueObjects;
using Game.Domain.UserAggregate.Entities;
using WorldDomination.Shared.Domain;

namespace Game.Domain.RoomAggregate.Entities
{
    public sealed class Organizer : RoomMember
    {
        //EF
        private Organizer() {}

        private Organizer(Guid creatorId, Guid gameRoomId, string name, string path)
            : base(creatorId, gameRoomId, name, path) { }

        private Organizer(Guid gameUserId, Guid roomId, string name, string path, GameRole gameRole, IdValueObject? countryId)
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