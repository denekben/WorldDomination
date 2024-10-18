using Game.Domain.ValueObjects;
using WorldDomination.Shared.Domain;

namespace Game.Domain.Entities
{
    public class RoomMember
    {
        public IdValueObject Id { get; private set; }
        public IdValueObject GameRoomId { get; private set; }
        public string Name { get; private set; }
        public string Username { get; private set; }
        public string ProfileImagePath { get; private set; }
        public DateTime? CreatedTime { get; private set; }
        public DateTime? UpdatedTime { get; private set; }

        // EF
        private RoomMember() { }

        private RoomMember(Guid id, Guid gameRoomId, string name, string username, string path) {
            Id = id;
            GameRoomId = gameRoomId;
            Name = name;
            Username = username;
            ProfileImagePath = path;
        }

        public static RoomMember Create(Guid id, Guid gameRoomId, string name, string username, string path)
        {
            return new RoomMember(id, gameRoomId, name, username, path);
        }
    }
}
