using Game.Domain.DomainModels.RoomAggregate.Abstractions;

namespace Game.Domain.RoomAggregate.Entities
{
    public sealed class Organizer : RoomMember
    {
        //EF
        private Organizer() {}

        private Organizer(Guid creatorId, Guid gameRoomId, string name, string path)
            : base(creatorId, gameRoomId, name, path) { }

        public static Organizer Create(Guid creatorId, Guid gameRoomId, string name, string path)
        {
            return new Organizer(creatorId, gameRoomId, name, path);
        }
    }
}
