using Game.Infrastructure.ReadModels.RoomAggregate;

namespace Game.Infrastructure.ReadModels.UserAggregate
{
    public sealed class GameUserReadModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string ProfileImagePath { get; private set; }
        public List<RoomReadModel> Rooms { get; private set; }
    }
}
