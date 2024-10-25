using WorldDomination.Shared.Domain;
using Game.Domain.RoomAggregate.Entities;
using Game.Domain.RoomAggregate.ValueObjects;

namespace Game.Domain.RoomAggregate.Abstractions
{
    public abstract class RoomMember
    {
        public IdValueObject Id { get; private set; }
        public string Name { get; private set; }
        public string ProfileImagePath { get; private set; }
        public IdValueObject RoomId { get; private set; }
        public Room Room { get; private set; }

        protected RoomMember() { }

        protected RoomMember(Guid roomId, string name, string path)
        {
            Id = Guid.NewGuid();
            RoomId = roomId;
            Name = name;
            ProfileImagePath = path;
        }
    }
}
