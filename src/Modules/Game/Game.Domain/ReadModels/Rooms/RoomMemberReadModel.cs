using Game.Domain.DomainModels.ReadModels.Games;
using Game.Domain.DomainModels.ReadModels.Users;

namespace Game.Domain.DomainModels.ReadModels.Rooms
{
    public abstract class RoomMemberReadModel
    {
        public Guid GameUserId { get; private set; }
        public Guid RoomId { get; private set; }
        public string Name { get; private set; }
        public string ProfileImagePath { get; private set; }
        public string GameRole { get; private set; }

        public RoomReadModel Room { get; private set; }
        public GameUserReadModel GameUser { get; private set; }
        public Guid CountryId { get; private set; }
        public CountryReadModel Country { get; private set; }
    }
}
