using Game.Domain.RoomAggregate.Abstractions;

namespace Game.Domain.RoomAggregate.Entities
{
    public sealed class Organizer : RoomMember
    {
        //EF
        private Organizer() {}

        private Organizer(Guid gameRoomId, string name, string path)
        : base(gameRoomId, name, path)
        {

        }

        public static Organizer Create(Guid gameRoomId, string name, string path)
        {
            return new Organizer(gameRoomId, name, path);
        }
    }
}
