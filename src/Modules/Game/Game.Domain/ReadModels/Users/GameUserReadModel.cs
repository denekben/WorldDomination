using Game.Domain.DomainModels.ReadModels.Rooms;

namespace Game.Domain.DomainModels.ReadModels.Users
{
    public sealed class GameUserReadModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string ProfileImagePath { get; private set; }
        public List<RoomReadModel> Rooms { get; private set; }
        public List<RoomMemberReadModel> CreatedMembers { get; private set; }
    }
}
