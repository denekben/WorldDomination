using Game.Domain.DomainModels.Rooms.Entities;
using WorldDomination.Shared.Domain;

namespace Game.Domain.DomainModels.Users.Entities
{
    public sealed class GameUser : DomainEntity
    {
        public IdValueObject Id { get; private set; }
        public string Name { get; private set; }
        public string ProfileImagePath { get; private set; }

        public List<Room> Rooms { get; private set; }
        public List<RoomMember> CreatedMembers { get; private set; }

        //EF
        private GameUser() { }

        private GameUser(Guid id, string name, string profileImagePath)
        {
            Id = id;
            Name = name;
            ProfileImagePath = profileImagePath;
        }

        public static GameUser Create(Guid id, string name, string profileImagePath)
        {
            return new GameUser(id, name, profileImagePath);
        }

        public void ChangeName(string name)
        {
            Name = name;
        }
    }
}
