using Game.Domain.DomainModels.Rooms.ValueObjects;

namespace Game.Domain.DomainModels.Rooms.Entities
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