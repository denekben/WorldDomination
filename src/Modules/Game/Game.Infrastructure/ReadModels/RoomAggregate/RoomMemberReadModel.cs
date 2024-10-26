namespace Game.Infrastructure.ReadModels.RoomAggregate
{
    public abstract class RoomMemberReadModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string ProfileImagePath { get; private set; }
        public Guid RoomId { get; private set; }
        public RoomReadModel Room { get; private set; }
    }
}
